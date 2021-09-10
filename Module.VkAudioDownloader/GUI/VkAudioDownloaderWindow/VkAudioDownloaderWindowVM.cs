using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Module.VkAudioDownloader.Entities;
using Module.VkAudioDownloader.Helpers;
using Module.VkAudioDownloader.Settings;
using Module.VkAudioDownloader.TagReplacer;
using MoreLinq;
using PropertyChanged;
using Root;
using Root.Helpers;
using Root.MVVM;
using VkNet.Abstractions;
using VkNet.Model.Attachments;

namespace Module.VkAudioDownloader.GUI.VkAudioDownloaderWindow
{
    // TODO отрефакторить класс
    [AddINotifyPropertyChangedInterface]
    public class VkAudioDownloaderWindowVM
    {
        #region Bindings

        public bool IsRefreshing { get; private set; }

        private RelayCommand? _refreshCmd;

        public RelayCommand RefreshCmd
            => _refreshCmd ??= new RelayCommand(_ => Refresh());

        // TODO проверить, используется ли вообще
        private RelayCommand? _applyCheckStateToSelectedCmd;

        public RelayCommand ApplyCheckStateToSelectedCmd
            => _applyCheckStateToSelectedCmd ??= new RelayCommand(arg =>
            {
                var argsArr = (object[]) arg;
                var triggered = (VkAudioVM) argsArr[0];
                var selectedObjects = (IList<object>) argsArr[1];
                var selected = selectedObjects
                    .OfType<VkAudioVM>()
                    .ToReadOnlyCollection();

                ApplyCheckStateToSelected(triggered, selected);
            });

        private RelayCommand? _applyCommand;

        public RelayCommand ApplyCommand
            => _applyCommand ??= new RelayCommand(async _ => await Apply());

        public bool IsApplying { get; set; }

        public ObservableCollection<BaseAudioVM> Audios { get; } = new();

        #endregion

        private readonly MBTagReplacer _tagReplacer = new();

        private readonly MusicBeeApiInterface _mbApi;
        private readonly IVkApi _vkApi;
        private readonly IMusicDownloaderSettings _settings;

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
            if (IsRefreshing)
                return;
            IsRefreshing = true;

            Audios.Clear();

            var mbAudios = GetLastMBAudios();

            var gotVkIds = Enumerable.ToHashSet(mbAudios
                .Select(item => item.VkId));

            if (gotVkIds.Count == 0)
            {
                MessageBox.Show("Was not found valid MB audios. Can't download vk audios.");
                return;
            }

            var vkAudios = await GetVkAudios(gotVkIds);

            Audios.AddRange(mbAudios);
            Audios.AddRange(vkAudios);

            IsRefreshing = false;
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
            return new MBAudioVM
            {
                Artist = _mbApi.Library_GetFileTag(filePath, MetaDataType.Artist),
                Title = _mbApi.Library_GetFileTag(filePath, MetaDataType.TrackTitle),
                Index = _mbApi.GetIndexOrDefault(filePath, -1),
                VkId = _mbApi.GetVkIdOrDefault(filePath, -1)
            };
        }

        private async Task<IReadOnlyCollection<VkAudioVM>> GetVkAudios(ICollection<long> gotVkIds, int maxDepth = 50)
        {
            return await _vkApi.Audio.AsAsyncEnumerable()
                .TakeWhile(audio => audio.Id is not null
                                    && !gotVkIds.Contains(audio.Id.Value))
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

        private static void ApplyCheckStateToSelected(VkAudioVM triggered, IReadOnlyCollection<VkAudioVM> selected)
        {
            if (selected.Contains(triggered))
            {
                selected
                    .Where(x => x != triggered)
                    .ForEach(x => x.IsSelected = triggered.IsSelected);
            }
        }

        private async Task Apply()
        {
            if (IsApplying)
                return;
            IsApplying = true;

            await ApplyDecorated();

            IsApplying = false;
        }

        private async Task ApplyDecorated()
        {
            if (_settings.DownloadDirTemplate.Length == 0)
            {
                MessageBox.Show("Download directory template is empty. Set it in settings.");
                return;
            }

            var items = MakeSomeItems();

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

            foreach (var (vkAudioVM, filePath) in items)
            {
                _mbApi.Library_AddFileToLibrary(filePath, LibraryCategory.Inbox);

                _mbApi.SetVkId(filePath, vkAudioVM.VkId, false);
                _mbApi.Library_SetFileTag(filePath, MetaDataType.Artist, vkAudioVM.Artist);
                _mbApi.Library_SetFileTag(filePath, MetaDataType.TrackTitle, vkAudioVM.Title);

                _mbApi.Library_CommitTagsToFile(filePath);
            }

            Refresh();
        }

        private IReadOnlyCollection<VkAudioVMWithFileSavePath> MakeSomeItems()
        {
            return Audios
                .OfType<VkAudioVM>()
                .Where(vkAudio => vkAudio.IsSelected)
                .Reverse()
                .Select(vkAudioVM =>
                {
                    // TODO add INDEX to tag replacer
                    _tagReplacer.SetReplaceValue(MBTagReplacer.Tag.Artist, vkAudioVM.Artist);
                    _tagReplacer.SetReplaceValue(MBTagReplacer.Tag.Title, vkAudioVM.Title);
                    var downloadDir = _tagReplacer.Prepare(_settings.DownloadDirTemplate);
                    downloadDir = PathEx.RemoveInvalidDirChars(downloadDir);

                    var fileName = _tagReplacer.Prepare(_settings.FileNameTemplate) + ".mp3";
                    fileName = PathEx.RemoveInvalidFileNameChars(fileName);

                    return new VkAudioVMWithFileSavePath(vkAudioVM, Path.Combine(downloadDir, fileName));
                })
                .ToReadOnlyCollection();
        }

        private static IReadOnlyCollection<Task> MakeDownloadTasks(IEnumerable<VkAudioVMWithFileSavePath> items)
        {
            return items
                .Select(someItem =>
                {
                    var directoryName = Path.GetDirectoryName(someItem.FilePath);
                    DirectoryHelper.CreateIfNotExists(directoryName
                                                      ?? throw new Exception("DirectoryName is null"));

                    return AudioDownloadHelper.DownloadAudioAsync(someItem.VM.Url, someItem.FilePath);
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
    }
}