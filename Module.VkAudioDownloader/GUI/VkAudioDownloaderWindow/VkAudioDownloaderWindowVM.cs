using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Module.VkAudioDownloader.Entities;
using Module.VkAudioDownloader.Helpers;
using Module.VkAudioDownloader.Settings;
using Module.VkAudioDownloader.TagReplacer;
using PropertyChanged;
using Root;
using Root.Helpers;
using Root.MVVM;
using VkNet.Abstractions;
using VkNet.Model.Attachments;

namespace Module.VkAudioDownloader.GUI.VkAudioDownloaderWindow
{
    [AddINotifyPropertyChangedInterface]
    public class VkAudioDownloaderWindowVM
    {
        #region Bindings

        private RelayCommand? _refreshCmd;
        public RelayCommand RefreshCmd
            => _refreshCmd ??= new RelayCommand(_ => Refresh());

        private RelayCommand? _applyCheckStateToSelectedCmd;
        public RelayCommand ApplyCheckStateToSelectedCmd
            => _applyCheckStateToSelectedCmd ??= new RelayCommand(arg =>
            {
                var argsArr = (object[]) arg!;
                var triggered = (VkAudioVM) argsArr[0];
                var selectedObjects = (IReadOnlyCollection<object>) argsArr[1];
                var selected = selectedObjects
                    .OfType<VkAudioVM>()
                    .ToReadOnlyCollection();

                ApplyCheckStateToSelected(triggered, selected);
            });

        public bool IsDownloading { get; set; }
        
        private RelayCommand? _downloadCommand;
        public RelayCommand DownloadCommand
            => _downloadCommand ??= new RelayCommand(
                async _ => await Download());
        
        public ObservableCollection<BaseAudioVM> Audios { get; } = new();

        #endregion

        private readonly MBTagReplacer _tagReplacer = new();

        private readonly MusicBeeApiInterface _mbApi;
        private readonly IVkApi _vkApi;
        private readonly IMusicDownloaderSettings _settings;

        private readonly Semaphore _refreshSemaphore = new(1, 1);
        private readonly Semaphore _applySemaphore = new(1, 1);

        public VkAudioDownloaderWindowVM(
            MusicBeeApiInterface mbApi,
            IVkApi vkApi,
            IMusicDownloaderSettings settings)
        {
            _mbApi = mbApi;
            _vkApi = vkApi;
            _settings = settings;
        }

        private async void Refresh()
        {
            if (!_refreshSemaphore.WaitOne(0))
            {
                return;
            }

            try
            {
                Audios.Clear();

                var mbAudios = GetLastMBAudios();

                var vkAudios = await GetVkAudios();

                Audios.AddRange(mbAudios);
                Audios.AddRange(vkAudios);
            }
            finally
            {
                _refreshSemaphore.Release();
            }
        }

        private async Task Download()
        {
            if (!_applySemaphore.WaitOne(0))
            {
                return;
            }

            try
            {
                IsDownloading = true;
                await Download_();
            }
            finally
            {
                _applySemaphore.Release();
                IsDownloading = false;
            }
        }

        private async Task Download_()
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

            Refresh();
        }
        
        private static void ApplyCheckStateToSelected(VkAudioVM triggered, IReadOnlyCollection<VkAudioVM> selected)
        {
            if (!selected.Contains(triggered))
            {
                return;
            }
            
            foreach (var vkAudioVM in selected.Where(x => x != triggered))
            {
                vkAudioVM.IsSelected = triggered.IsSelected;
            }
        }

        private IReadOnlyCollection<MBAudioVM> GetLastMBAudios(int count = 20)
        {
            return GetFilePathsOrDefault(Array.Empty<string>())
                .Select(FilePathToMBAudioVM)
                .OrderByDescending(x => x.Index)
                .Take(count)
                .ToReadOnlyCollection();
        }

        private IReadOnlyCollection<string> GetFilePathsOrDefault(IReadOnlyCollection<string> defaultFilePaths)
        {
            return _mbApi.Library_QueryFilesEx("", out var filePaths)
                ? filePaths
                : defaultFilePaths;
        }

        private MBAudioVM FilePathToMBAudioVM(string filePath)
        {
            return new()
            {
                Artist = _mbApi.Library_GetFileTag(filePath, MetaDataType.Artist),
                Title = _mbApi.Library_GetFileTag(filePath, MetaDataType.TrackTitle),
                Index = _mbApi.GetIndexOrDefault(filePath, -1),
                VkId = _mbApi.GetVkIdOrDefault(filePath, -1)
            };
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

        private static VkAudioVM AudioToVkAudioVM(Audio audio, int index)
        {
            var convertRes = VkApiHelper.ConvertToMp3(audio.Url.AbsoluteUri, out var mp3Url);
            return new VkAudioVM
            {
                IsSelected = true,
                Artist = audio.Artist,
                Title = audio.Title,
                InsideIndex = index,
                Url = mp3Url,
                IsCorraptedUrl = !convertRes,
                VkId = audio.Id
                       ?? throw new Exception("This message is not being to be shown (If all is alright).")
            };
        }
        
        private IReadOnlyCollection<VkAudioVMWithFileSavePath> GetItemsForDownload()
        {
            return Audios
                .OfType<VkAudioVM>()
                .Where(vkAudio => vkAudio.IsSelected)
                .Reverse()
                .Select(AddFileSavePath)
                .ToReadOnlyCollection();
        }

        private VkAudioVMWithFileSavePath AddFileSavePath(VkAudioVM vkAudioVM)
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

                    return AudioDownloadHelper.DownloadAudioAsync(item.VM.Url, item.FilePath);
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
                MessageBox.Show($"Error downloading file: {message}.");
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
}