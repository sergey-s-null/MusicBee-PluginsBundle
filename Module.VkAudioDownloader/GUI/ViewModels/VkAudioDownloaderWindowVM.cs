using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Mead.MusicBee.Api.Services.Abstract;
using Mead.MusicBee.Enums;
using Module.MusicBee.Extension.Helpers;
using Module.Mvvm.Extension;
using Module.Vk.Helpers;
using Module.VkAudioDownloader.Entities;
using Module.VkAudioDownloader.Extensions;
using Module.VkAudioDownloader.GUI.AbstractViewModels;
using Module.VkAudioDownloader.Helpers;
using Module.VkAudioDownloader.Services.Abstract;
using Module.VkAudioDownloader.Settings;
using Module.VkAudioDownloader.TagReplacer;
using PropertyChanged;
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
    private readonly IMusicDownloaderSettings _settings;
    private readonly IVkAudiosService _vkAudiosService;
    private readonly IAudioDownloader _audioDownloader;

    private readonly Semaphore _refreshSemaphore = new(1, 1);
    private readonly Semaphore _applySemaphore = new(1, 1);

    public VkAudioDownloaderWindowVM(
        IMusicBeeApi mbApi,
        IMusicDownloaderSettings settings,
        IVkAudiosService vkAudiosService,
        IAudioDownloader audioDownloader)
    {
        _mbApi = mbApi;
        _settings = settings;
        _vkAudiosService = vkAudiosService;
        _audioDownloader = audioDownloader;
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

        var audios = GetValidSelectedAudios();

        var downloadResult = await _audioDownloader.DownloadBatchAsync(MapToAudiosToDownload(audios));

        CommitDownload(audios, downloadResult.Results);

        DisplayErrorsIfExists(downloadResult);

        RefreshInternal();
    }

    private async Task<IReadOnlyCollection<IVkAudioVM>> GetVkAudios()
    {
        var audiosNotInLibrary = _vkAudiosService.GetVkAudiosNotContainingInLibraryAsync()
            .Select(x => MapToViewModel(x, false));
        var audiosInIncoming = _vkAudiosService.GetVkAudiosContainingInIncomingAsync()
            .Select(x => MapToViewModel(x, true));

        return await audiosNotInLibrary
            .Union(audiosInIncoming)
            .ToListAsync();
    }

    private static IVkAudioVM MapToViewModel(Audio audio, bool isInIncoming)
    {
        var convertRes = VkApiHelper.ConvertToMp3(audio.Url.AbsoluteUri, out var mp3Url);

        return new VkAudioVM(
            audio.Id!.Value,
            audio.Artist,
            audio.Title,
            new VkAudioUrlVM(mp3Url, !convertRes),
            isInIncoming
        )
        {
            IsSelected = true
        };
    }

    private IReadOnlyList<IVkAudioVM> GetValidSelectedAudios()
    {
        return Audios
            .Where(vkAudio => vkAudio.IsSelected
                              && vkAudio.Url is not null)
            .ToList();
    }

    private IReadOnlyList<AudioToDownload> MapToAudiosToDownload(IReadOnlyList<IVkAudioVM> audios)
    {
        return audios
            .Select(MapToAudioToDownload)
            .ToList();
    }

    private AudioToDownload MapToAudioToDownload(IVkAudioVM vkAudio)
    {
        var destinationPath = GetAudioDownloadPath(vkAudio);
        return new AudioToDownload(vkAudio.Url!.Value, destinationPath);
    }

    private string GetAudioDownloadPath(IVkAudioVM vkAudio)
    {
        _tagReplacer.SetReplaceValue(MBTagReplacer.Tag.Artist, vkAudio.Artist);
        _tagReplacer.SetReplaceValue(MBTagReplacer.Tag.Title, vkAudio.Title);

        var downloadDir = _tagReplacer.Prepare(_settings.DownloadDirTemplate);
        var fileName = _tagReplacer.Prepare(_settings.FileNameTemplate) + ".mp3";

        downloadDir = PathEx.RemoveInvalidDirChars(downloadDir);
        fileName = PathEx.RemoveInvalidFileNameChars(fileName);

        return Path.Combine(downloadDir, fileName);
    }

    private void DisplayErrorsIfExists(BatchDownloadResult downloadResult)
    {
        var errorResults = downloadResult.Results
            .Where(x => !x.IsSuccess)
            .ToList();

        if (errorResults.Count == 0)
        {
            return;
        }

        var message = string.Join(
            "\n\n",
            errorResults.Select(x =>
                $"Error occured while downloading from {x.Url} to \"{x.DestinationPath}\".")
        );

        // TODO display with message in TextBox
        MessageBox.Show(message);
    }

    private void CommitDownload(IReadOnlyList<IVkAudioVM> audios, IReadOnlyList<AudioDownloadResult> downloadResults)
    {
        foreach (var (vkAudioVM, result) in audios.Zip(downloadResults))
        {
            if (!result.IsSuccess)
            {
                continue;
            }

            var filePath = result.DestinationPath;

            _mbApi.Library_AddFileToLibrary(filePath, LibraryCategory.Inbox);

            _mbApi.SetVkId(filePath, vkAudioVM.VkId, false);
            _mbApi.Library_SetFileTag(filePath, MetaDataType.Artist, vkAudioVM.Artist);
            _mbApi.Library_SetFileTag(filePath, MetaDataType.TrackTitle, vkAudioVM.Title);

            _mbApi.Library_CommitTagsToFile(filePath);
        }
    }
}