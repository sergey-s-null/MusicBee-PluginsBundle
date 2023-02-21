using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Mead.MusicBee.Api.Services.Abstract;
using Mead.MusicBee.Enums;
using Module.Core.Helpers;
using Module.MusicBee.Extension.Helpers;
using Module.Mvvm.Extension;
using Module.Vk.Helpers;
using Module.VkAudioDownloader.Entities;
using Module.VkAudioDownloader.GUI.AbstractViewModels;
using Module.VkAudioDownloader.Helpers;
using Module.VkAudioDownloader.Settings;
using Module.VkAudioDownloader.TagReplacer;
using PropertyChanged;
using VkNet.Abstractions;
using VkNet.Model.Attachments;

namespace Module.VkAudioDownloader.GUI.ViewModels;

[AddINotifyPropertyChangedInterface]
public sealed class VkAudioDownloaderWindowVM : IVkAudioDownloaderWindowVM
{
    public bool IsDownloading { get; private set; }

    public IList<IVkAudioVM> Audios { get; } = new ObservableCollection<IVkAudioVM>();

    public ICommand Refresh => _refreshCmd ??= new RelayCommand(_ => RefreshInternal());

    public ICommand Download => _downloadCommand ??=
        new RelayCommand(async _ => await DownloadAsync());

    private ICommand? _refreshCmd;
    private ICommand? _downloadCommand;

    private readonly MBTagReplacer _tagReplacer = new();

    private readonly IMusicBeeApi _mbApi;
    private readonly IVkApi _vkApi;
    private readonly IMusicDownloaderSettings _settings;

    private readonly Semaphore _refreshSemaphore = new(1, 1);
    private readonly Semaphore _applySemaphore = new(1, 1);

    public VkAudioDownloaderWindowVM(
        IMusicBeeApi mbApi,
        IVkApi vkApi,
        IMusicDownloaderSettings settings)
    {
        _mbApi = mbApi;
        _vkApi = vkApi;
        _settings = settings;
    }

    private async void RefreshInternal()
    {
        if (!_refreshSemaphore.WaitOne(0))
        {
            return;
        }

        try
        {
            Audios.Clear();

            var vkAudios = await GetVkAudios();

            Audios.AddRange(vkAudios);
        }
        finally
        {
            _refreshSemaphore.Release();
        }
    }

    private async Task DownloadAsync()
    {
        if (!_applySemaphore.WaitOne(0))
        {
            return;
        }

        try
        {
            IsDownloading = true;
            await DownloadCore();
        }
        finally
        {
            _applySemaphore.Release();
            IsDownloading = false;
        }
    }

    private async Task DownloadCore()
    {
        if (_settings.DownloadDirTemplate.Length == 0)
        {
            MessageBox.Show("Download directory template is empty. Set it in settings.");
            return;
        }

        var items = GetItemsForDownload();

        var downloadTasks = MakeDownloadTasks(items);

        try
        {
            await Task.WhenAll(downloadTasks);
        }
        catch (Exception e)
        {
            HandleDownloadError(items, e.Message);
            return;
        }

        CommitDownload(items);

        RefreshInternal();
    }

    private async Task<IReadOnlyCollection<VkAudioVM>> GetVkAudios(int maxDepth = 50)
    {
        var vkIdsFromLibrary = _mbApi.EnumerateVkIds().ToHashSet();

        return await _vkApi.Audio.AsAsyncEnumerable()
            .TakeWhile(audio => audio.Id is not null
                                && !vkIdsFromLibrary.Contains(audio.Id.Value))
            .Take(maxDepth)
            .Where(audio => audio.Url is not null)
            .Select(AudioToVkAudioVM)
            .ToReadOnlyCollectionAsync();
    }

    private static VkAudioVM AudioToVkAudioVM(Audio audio)
    {
        var convertRes = VkApiHelper.ConvertToMp3(audio.Url.AbsoluteUri, out var mp3Url);

        if (audio.Id is null)
        {
            throw new Exception("This message is not being to be shown (If all is alright).");
        }

        return new VkAudioVM(
            audio.Id.Value,
            audio.Artist,
            audio.Title,
            mp3Url,
            !convertRes,
            false // todo
        )
        {
            IsSelected = true
        };
    }

    private IReadOnlyCollection<VkAudioVMWithFileSavePath> GetItemsForDownload()
    {
        return Audios
            .Where(vkAudio => vkAudio.IsSelected)
            .Reverse()
            .Select(AddFileSavePath)
            .ToReadOnlyCollection();
    }

    private VkAudioVMWithFileSavePath AddFileSavePath(IVkAudioVM vkAudioVM)
    {
        _tagReplacer.SetReplaceValue(MBTagReplacer.Tag.Artist, vkAudioVM.Artist);
        _tagReplacer.SetReplaceValue(MBTagReplacer.Tag.Title, vkAudioVM.Title);

        var downloadDir = _tagReplacer.Prepare(_settings.DownloadDirTemplate);
        var fileName = _tagReplacer.Prepare(_settings.FileNameTemplate) + ".mp3";

        downloadDir = PathEx.RemoveInvalidDirChars(downloadDir);
        fileName = PathEx.RemoveInvalidFileNameChars(fileName);

        return new VkAudioVMWithFileSavePath(vkAudioVM, Path.Combine(downloadDir, fileName));
    }

    private static IReadOnlyCollection<Task> MakeDownloadTasks(IEnumerable<VkAudioVMWithFileSavePath> items)
    {
        return items
            .Select(item =>
            {
                var directoryName = Path.GetDirectoryName(item.FilePath);
                DirectoryHelper.CreateIfNotExists(directoryName
                                                  ?? throw new Exception("DirectoryName is null"));

                return AudioDownloadHelper.DownloadAudioAsync(item.VkAudio.Url, item.FilePath);
            })
            .ToReadOnlyCollection();
    }

    private static void HandleDownloadError(IReadOnlyCollection<VkAudioVMWithFileSavePath> items, string message)
    {
        var notDeleted = items
            .Where(x => File.Exists(x.FilePath))
            .Where(x => !FileEx.TryDelete(x.FilePath))
            .Select(x => x.FilePath)
            .ToReadOnlyCollection();

        if (notDeleted.Count > 0)
        {
            // TODO display with message in TextBox
            var dialogMessage =
                "Error downloading files.\n\n" +
                $"Message: {message}\n\n" +
                "These files was not deleted:\n" +
                string.Join(Environment.NewLine, notDeleted);
            MessageBox.Show(dialogMessage);
        }
        else
        {
            MessageBox.Show($"Error downloading file: {message}.");
        }
    }

    private void CommitDownload(IEnumerable<VkAudioVMWithFileSavePath> items)
    {
        foreach (var (vkAudioVM, filePath) in items)
        {
            _mbApi.Library_AddFileToLibrary(filePath, LibraryCategory.Inbox);

            _mbApi.SetVkId(filePath, vkAudioVM.VkId, false);
            _mbApi.Library_SetFileTag(filePath, MetaDataType.Artist, vkAudioVM.Artist);
            _mbApi.Library_SetFileTag(filePath, MetaDataType.TrackTitle, vkAudioVM.Title);

            _mbApi.Library_CommitTagsToFile(filePath);
        }
    }
}