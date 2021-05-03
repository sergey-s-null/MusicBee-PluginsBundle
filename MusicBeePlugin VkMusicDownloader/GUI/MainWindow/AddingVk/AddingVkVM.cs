using MusicBeePlugin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VkMusicDownloader.Abstractions;
using VkMusicDownloader.Ex;
using VkNet;
using VkNet.Model.Attachments;

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
            => _refreshCmd ?? (_refreshCmd = new RelayCommand(_ => Refresh()));

        private RelayCommand _applyCheckStateToSelectedCmd;
        public RelayCommand ApplyCheckStateToSelectedCmd
            => _applyCheckStateToSelectedCmd ?? (_applyCheckStateToSelectedCmd = new RelayCommand(arg =>
            {
                object[] arr = (object[])arg;
                VkAudioVM triggered = (VkAudioVM)arr[0];
                IList<object> selectedObjects = (IList<object>)arr[1];
                IEnumerable<VkAudioVM> selected = selectedObjects
                    .Where(obj => obj is VkAudioVM)
                    .Select(obj => (VkAudioVM)obj);

                ApplyCheckStateToSelected(triggered, selected);
            }));

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
            => _audios ?? (_audios = new ObservableCollection<BaseAudioVM>());

        #endregion

        private VkApi _vkApi = null;
        public VkApi VkApi
        {
            get => _vkApi;
            set => _vkApi = value;
        }

        private MBTagReplacer _tagReplacer = new MBTagReplacer();

        private async void Refresh()
        {
            if (IsRefreshing)
                return;
            IsRefreshing = true;

            Audios.Clear();

            MBAudioVM[] mbAudios = GetLastMBAudios(out long lastVkId);

            if (lastVkId == -1)
            {
                MessageBox.Show("Was not found valid MB audios. Can't download vk audios.");
                return;
            }

            List<VkAudioVM> vkAudios = await GetVkAudios(lastVkId);

            Audios.AddRange(mbAudios);
            Audios.AddRange(vkAudios);

            IsRefreshing = false;
        }

        private MBAudioVM[] GetLastMBAudios(out long lastVkId, int count = 20)
        {
            if (!Plugin.MBApiInterface.Library_QueryFilesEx("", out string[] paths))
            {
                lastVkId = -1;
                return Array.Empty<MBAudioVM>();
            }

            // path -> (index, VkId, path)
            var list = paths.Select(path =>
            {
                if (!Plugin.TryGetIndex(path, out int index))
                    return null;
                if (!Plugin.TryGetVkId(path, out long vkId))
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

            lastVkId = -1;
            foreach (var item in list)
            {
                if (item.VkId != -1)
                {
                    lastVkId = item.VkId;
                    break;
                }
            }

            MBAudioVM[] result = new MBAudioVM[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                result[i] = new MBAudioVM()
                {
                    Artist = Plugin.MBApiInterface.Library_GetFileTag(list[i].Path, Plugin.MetaDataType.Artist),
                    Title = Plugin.MBApiInterface.Library_GetFileTag(list[i].Path, Plugin.MetaDataType.TrackTitle),
                    Index = list[i].Index,
                    VkId = list[i].VkId
                };
            }
            return result;
        }

        private async Task<List<VkAudioVM>> GetVkAudios(long lastVkId, int maxDepth = 50)
        {
            List<VkAudioVM> result = new List<VkAudioVM>();

            if (VkApi is null)
            {
                MessageBox.Show("VkApi is null. Something went wrong(");
                return result;
            }

            int insideIndex = 0;
            IAsyncEnumerator<Audio> enumerator = VkApi.Audio.GetAsyncEnumerator();
            while (await enumerator.MoveNextAsync())
            {
                Audio audio = enumerator.Current;

                if (audio.Id is null || audio.Id == lastVkId)
                    break;
                if (--maxDepth == 0)
                {
                    MessageBox.Show("Vk audios depth has achieved.", "Warning!");
                    break;
                }

                bool convertRes = IVkApiEx.ConvertToMp3(audio.Url.AbsoluteUri, out string mp3Url);
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
            string downloadDirTemplate = Plugin.Settings.DownloadDirTemplate;
            if (downloadDirTemplate.Length == 0)
            {
                MessageBox.Show("Download directory is empty. Set it in settings.");
                return;
            }

            int lastIndex = (Audios.First(audio => audio is MBAudioVM) as MBAudioVM).Index;

            var items = Audios
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

                    string fileName = _tagReplacer.Prepare(Plugin.Settings.FileNameTemplate) + ".mp3";
                    fileName = PathEx.RemoveInvalidFileNameChars(fileName);

                    return new
                    {
                        VM = vm,
                        Index = index,
                        WebClient = new WebClient(),
                        FilePath = Path.Combine(downloadDir, fileName)
                    };
                })
                .ToArray();

            List<Task> downloadTasks = new List<Task>();
            foreach (var item in items)
            {
                string dirName = new FileInfo(item.FilePath).DirectoryName;
                if (!DirectoryEx.TryCreateDirectory(dirName))
                {
                    MessageBox.Show($"Error create directory: {dirName}.");
                    return;
                }

                downloadTasks.Add(item.WebClient.DownloadFileTaskAsync(item.VM.Url, item.FilePath));
            }

            // wait for downloading has done
            try
            {
                await Task.WhenAll(downloadTasks);
            }
            catch (Exception e)
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
                    string message = "Error downloading files. These files was not deleted:";
                    message += notDeleted.Aggregate((a, b) => $"\n{a}\n{b}");
                    MessageBox.Show(message);
                }
                else
                    MessageBox.Show($"Error downloading file: {e.Message}.");
                return;
            }
            finally
            {
                foreach (var item in items)
                    item.WebClient.Dispose();
            }

            foreach (var item in items)
            {
                Plugin.CalcIndices(item.Index, out int i1, out int i2);
                Plugin.MBApiInterface.Library_AddFileToLibrary(item.FilePath, Plugin.LibraryCategory.Music);
                Plugin.SetVkId(item.FilePath, item.VM.VkId, false);
                Plugin.SetIndex(item.FilePath, item.Index, false);
                Plugin.SetIndex1(item.FilePath, i1, false);
                Plugin.SetIndex2(item.FilePath, i2, false);
                Plugin.MBApiInterface.Library_SetFileTag(item.FilePath, Plugin.MetaDataType.Artist, item.VM.Artist);
                Plugin.MBApiInterface.Library_SetFileTag(item.FilePath, Plugin.MetaDataType.TrackTitle, item.VM.Title);
                Plugin.MBApiInterface.Library_CommitTagsToFile(item.FilePath);
            }

            Refresh();

        }
    }
}
