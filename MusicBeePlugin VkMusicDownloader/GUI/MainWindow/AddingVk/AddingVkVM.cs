using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MusicBeePlugin;
using VkMusicDownloader.Ex;
using VkMusicDownloader.Settings;
using VkNet;

namespace VkMusicDownloader.GUI
{
    public class AddingVkVM : BaseViewModel
    {
        #region Bindings

        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set
            {
                _isRefreshing = value;
                NotifyPropChanged(nameof(IsRefreshing));
            }
        }

        private RelayCommand _refreshCmd;
        public RelayCommand RefreshCmd
            => _refreshCmd ??= new RelayCommand(_ => Refresh());

        private RelayCommand _applyCheckStateToSelectedCmd;
        public RelayCommand ApplyCheckStateToSelectedCmd
            => _applyCheckStateToSelectedCmd ??= new RelayCommand(arg =>
            {
                var argsArr = (object[])arg;
                var triggered = (VkAudioVM)argsArr[0];
                var selectedObjects = (IList<object>)argsArr[1];
                var selected = selectedObjects
                    .OfType<VkAudioVM>();

                ApplyCheckStateToSelected(triggered, selected);
            });

        private RelayCommand _applyCommand;
        public RelayCommand ApplyCommand
            => _applyCommand ?? (_applyCommand = new RelayCommand(_ => Apply()));

        private bool _isApplying = false;
        public bool IsApplying
        {
            get => _isApplying;
            set
            {
                _isApplying = value;
                NotifyPropChanged(nameof(IsApplying));
            }
        }

        private ObservableCollection<BaseAudioVM> _audios;
        public ObservableCollection<BaseAudioVM> Audios
            => _audios ??= new ObservableCollection<BaseAudioVM>();

        #endregion
        
        private MBTagReplacer _tagReplacer = new MBTagReplacer();
        
        private readonly Plugin.MusicBeeApiInterface _mbApi;
        private readonly VkApi _vkApi;
        private readonly IMusicDownloaderSettings _settings;
        
        public AddingVkVM(
            Plugin.MusicBeeApiInterface mbApi,
            VkApi vkApi,
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

            MBAudioVM[] mbAudios = GetLastMBAudios(out var gotVkIds);

            if (gotVkIds.Count == 0)
            {
                MessageBox.Show("Was not found valid MB audios. Can't download vk audios.");
                return;
            }

            List<VkAudioVM> vkAudios = await GetVkAudios(gotVkIds);

            Audios.AddRange(mbAudios);
            Audios.AddRange(vkAudios);

            IsRefreshing = false;
        }

        private MBAudioVM[] GetLastMBAudios(out ISet<long> gotVkIds, int count = 20)
        {
            if (!_mbApi.Library_QueryFilesEx("", out string[] paths))
            {
                gotVkIds = new HashSet<long>();
                return Array.Empty<MBAudioVM>();
            }

            // path -> (index, VkId, path)
            var list = paths.Select(path =>
            {
                if (!_mbApi.TryGetIndex(path, out int index))
                    return null;
                if (!_mbApi.TryGetVkId(path, out long vkId))
                    vkId = -1;

                return new
                {
                    Index = index,
                    VkId = vkId,
                    Path = path
                };
            })
            .Where(item => item is object)
            .ToList();

            list.Sort((a, b) => b.Index.CompareTo(a.Index));

            if (list.Count > count)
                list.RemoveRange(count, list.Count - count);

            gotVkIds = list
                .Select(item => item.VkId)
                .ToHashSet();

            MBAudioVM[] result = new MBAudioVM[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                result[i] = new MBAudioVM()
                {
                    Artist = _mbApi.Library_GetFileTag(list[i].Path, Plugin.MetaDataType.Artist),
                    Title = _mbApi.Library_GetFileTag(list[i].Path, Plugin.MetaDataType.TrackTitle),
                    Index = list[i].Index,
                    VkId = list[i].VkId
                };
            }
            return result;
        }

        private async Task<List<VkAudioVM>> GetVkAudios(ISet<long> gotVkIds, int maxDepth = 50)
        {
            var result = new List<VkAudioVM>();
            
            var insideIndex = 0;
            var audiosEnumerator = _vkApi.Audio.GetAsyncEnumerator();
            while (await audiosEnumerator.MoveNextAsync())
            {
                var  audio = audiosEnumerator.Current;

                
                if (!audio.Id.HasValue || gotVkIds.Contains(audio.Id.Value))
                    break;
                if (--maxDepth == 0)
                {
                    MessageBox.Show("Vk audios depth has achieved.", "Warning!");
                    break;
                }
                if (audio.Url is null)// TODO Url with ? in class definition
                    continue;

                var convertRes = IVkApiEx.ConvertToMp3(audio.Url.AbsoluteUri, out string mp3Url);
                result.Add(new VkAudioVM()
                {
                    IsSelected = true,
                    Artist = audio.Artist,
                    Title = audio.Title,
                    InsideIndex = insideIndex++,
                    Url = mp3Url,
                    IsCorraptedUrl = !convertRes,
                    VkId = (long)audio.Id
                });
            }
            return result;
        }

        private void ApplyCheckStateToSelected(VkAudioVM triggered, IEnumerable<VkAudioVM> selected)
        {
            if (!selected.Contains(triggered))
                return;

            foreach (VkAudioVM audioVM in selected)
            {
                if (triggered != audioVM)
                    audioVM.IsSelected = triggered.IsSelected;
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
            var downloadDirTemplate = _settings.DownloadDirTemplate;
            if (downloadDirTemplate.Length == 0)
            {
                MessageBox.Show("Download directory is empty. Set it in settings.");
                return;
            }

            int lastIndex = (Audios.First(audio => audio is MBAudioVM) as MBAudioVM).Index;

            SomeItem[] items = MakeSomeItems();

            if (!TryMakeDownloadTasks(items, out Task[] downloadTasks))
                return;

            // wait for downloading has done
            try
            {
                await Task.WhenAll(downloadTasks);
            }
            catch (Exception e)
            {
                HandleDownloadError(e.Message);
                return;
            }

            foreach (var item in items)
            {
                Plugin.CalcIndices(item.Index, out int i1, out int i2);
                _mbApi.Library_AddFileToLibrary(item.FilePath, Plugin.LibraryCategory.Music);
                _mbApi.SetVkId(item.FilePath, item.VM.VkId, false);
                _mbApi.SetIndex(item.FilePath, item.Index, false);
                _mbApi.SetIndex1(item.FilePath, i1, false);
                _mbApi.SetIndex2(item.FilePath, i2, false);
                _mbApi.Library_SetFileTag(item.FilePath, Plugin.MetaDataType.Artist, item.VM.Artist);
                _mbApi.Library_SetFileTag(item.FilePath, Plugin.MetaDataType.TrackTitle, item.VM.Title);
                _mbApi.Library_CommitTagsToFile(item.FilePath);
            }

            Refresh();

            #region Subfunctions

            SomeItem[] MakeSomeItems()
            {
                return Audios
                    .Where(audio => audio is VkAudioVM vkAudio && vkAudio.IsSelected)
                    .Select(audio => (VkAudioVM)audio)
                    .Reverse()
                    .Select((vm, i) =>
                    {
                        int index = lastIndex + i + 1;
                        Plugin.CalcIndices(index, out int i1, out int i2);
                        string i1Str = i1.ToString().PadLeft(2, '0');
                        string i2Str = i2.ToString().PadLeft(2, '0');

                        // TODOL add INDEX to tag replacer
                        _tagReplacer.SetValues(i1Str, i2Str, vm.Artist, vm.Title);
                        string downloadDir = _tagReplacer.Prepare(downloadDirTemplate);
                        downloadDir = PathEx.RemoveInvalidDirChars(downloadDir);

                        string fileName = _tagReplacer.Prepare(_settings.FileNameTemplate) + ".mp3";
                        fileName = PathEx.RemoveInvalidFileNameChars(fileName);

                        return new SomeItem(vm, index, Path.Combine(downloadDir, fileName));
                    })
                    .ToArray();
            }

            bool TryMakeDownloadTasks(SomeItem[] items, out Task[] downloadTasks)
            {
                downloadTasks = new Task[items.Length];
                for (int i = 0; i < downloadTasks.Length; i++)
                {
                    string dirName = new FileInfo(items[i].FilePath).DirectoryName;
                    if (!DirectoryEx.TryCreateDirectory(dirName))
                    {
                        MessageBox.Show($"Error create directory: {dirName}.");
                        return false;
                    }

                    downloadTasks[i] = WebVk.DownloadAudioAsync(items[i].VM.Url, items[i].FilePath);
                }
                return true;
            }

            void HandleDownloadError(string message)
            {
                List<string> notDeleted = new List<string>();
                foreach (var item in items)
                {
                    if (File.Exists(item.FilePath))
                        if (!FileEx.TryDelete(item.FilePath))
                            notDeleted.Add(item.FilePath);
                }

                if (notDeleted.Count > 0)
                {
                    string dialogMessage = "Error downloading files. These files was not deleted:";
                    dialogMessage += notDeleted.Aggregate((a, b) => $"\n{a}\n{b}");
                    MessageBox.Show(dialogMessage);
                }
                else
                    MessageBox.Show($"Error downloading file: {message}.");
            }

            #endregion
        }

        #region SubClasses

        private class SomeItem
        {
            public VkAudioVM VM { get; private set; }
            public int Index { get; private set; }
            public string FilePath { get; private set; }

            public SomeItem(VkAudioVM vm, int index, string filePath)
            {
                VM = vm;
                Index = index;
                FilePath = filePath;
            }
        }

        #endregion
    }
}
