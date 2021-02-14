using MusicBeePlugin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

#pragma warning disable CS4014

namespace MusicBeePlugin_VkMusicDownloader
{
    class MainWindowViewModel : BaseViewModel
    {
        #region Bindings

        private RelayCommand _autoCheckCommand;
        public RelayCommand AutoCheckCommand
            => _autoCheckCommand ?? (_autoCheckCommand = new RelayCommand(_ => AutoCheck()));

        private RelayCommand _refreshVkAudioCommand;
        public RelayCommand RefreshVkAudioCommand
            => _refreshVkAudioCommand ?? (_refreshVkAudioCommand = new RelayCommand(_ => RefreshVkAudioList()));

        private ObservableCollection<VkAudioVM> _vkAudioList;
        public ObservableCollection<VkAudioVM> VkAudioList
            => _vkAudioList ?? (_vkAudioList = new ObservableCollection<VkAudioVM>());

        private bool _isLoadingVkAudio = false;
        public bool IsLoadingVkAudio
        {
            get => _isLoadingVkAudio;
            set
            {
                _isLoadingVkAudio = value;
                NotifyPropChanged(nameof(IsLoadingVkAudio));
            }
        }
        
        private RelayCommand _next10AudiosCommand;
        public RelayCommand Next10AudiosCommand
            => _next10AudiosCommand ?? (_next10AudiosCommand = new RelayCommand(_ => Next10AudiosAsync()));

        private ObservableCollection<MBAudioVM> _mbAudioList;
        public ObservableCollection<MBAudioVM> MBAudioList
            => _mbAudioList ?? (_mbAudioList = new ObservableCollection<MBAudioVM>());

        private RelayCommand _applyCheckStateToSelectedCommand;
        public RelayCommand ApplyCheckStateToSelectedCommand
            => _applyCheckStateToSelectedCommand ?? (_applyCheckStateToSelectedCommand = new RelayCommand(arg =>
            {
                object[] arr = arg as object[];
                if (arr is null || arr.Length != 2)
                    return;
                VkAudioVM viewModel = arr[0] as VkAudioVM;
                IList<object> selected = arr[1] as IList<object>;
                if (viewModel is null || selected is null)
                    return;

                ApplyCheckStateToSelected(viewModel, selected);
            }));

        private RelayCommand _refreshMBAudioListCommand;
        public RelayCommand RefreshMBAudioListCommand
            => _refreshMBAudioListCommand ?? (_refreshMBAudioListCommand = new RelayCommand(_ => RefreshMBAudioList()));

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

        private RelayCommand _applyCommand;
        public RelayCommand ApplyCommand
            => _applyCommand ?? (_applyCommand = new RelayCommand(_ => Apply()));

        #endregion

        private VkAudioApi _vkApi;
        private int _audioDataShift = 0;
        private MBTagReplacer _tagReplacer = new MBTagReplacer();

        public MainWindowViewModel(VkAudioApi vkApi)
        {
            _vkApi = vkApi;
            Next10AudiosAsync();
            RefreshMBAudioList();
        }

        private void AutoCheck()
        {
            bool found = false;
            for (int i = 0; i < Math.Min(3, MBAudioList.Count); ++i)
            {
                for (int j = 0; j < VkAudioList.Count; ++j)
                {
                    if (MBAudioList[i].VkId == VkAudioList[j].Id)
                    {
                        for (int k = 0; k < VkAudioList.Count; ++k)
                                VkAudioList[k].IsSelected = k < j - i;
                        found = true;
                        break;
                    }
                }
                if (found) break;
            }
        }

        private void RefreshVkAudioList()
        {
            _vkApi.ClearFirstMusicData();
            _audioDataShift = 0;
            VkAudioList.Clear();
            Next10AudiosAsync();
        }

        private void ApplyCheckStateToSelected(VkAudioVM triggeredViewModel, IList<object> selectedViewModels)
        {
            if (!selectedViewModels.Contains(triggeredViewModel))
                return;

            foreach (object item in selectedViewModels)
            {
                if (item is VkAudioVM selected && selected != triggeredViewModel)
                    selected.IsSelected = triggeredViewModel.IsSelected;
            }
        }

        private async Task Next10AudiosAsync()
        {
            if (IsLoadingVkAudio)
                return;
            IsLoadingVkAudio = true;

            try
            {
                List<VkMusicData> dataList = await _vkApi.GetAudioDataAsync(_audioDataShift, 10);
                foreach (var data in dataList)
                {
                    VkAudioList.Add(new VkAudioVM(data.Url)
                    {
                        Id = data.Id,
                        Artist = data.Artist,
                        Title = data.Title,
                        Duration = data.Duration
                    });
                }
                _audioDataShift += 10;
            }
            finally
            {
                IsLoadingVkAudio = false;
            }
        }

        private void RefreshMBAudioList()
        {
            MBAudioList.Clear();

            if (!Plugin.MBApiInterface.Library_QueryFilesEx("", out string[] paths))
                return;

            var list = paths.Select(path =>
            {
                if (!Plugin.TryGetIndex(path, out int index))
                    return null;
                if (!Plugin.TryGetVkId(path, out string vkId))
                    vkId = "";

                return new
                {
                    Index = index,
                    VkId = vkId,
                    Path = path
                };
            }).ToList();

            list.RemoveAll(a => a is null);
            list.Sort((a, b) => b.Index.CompareTo(a.Index));

            if (list.Count > 20)
                list.RemoveRange(20, list.Count - 20);

            foreach (var item in list)
            {
                MBAudioList.Add(new MBAudioVM
                {
                    Index = item.Index,
                    VkId = item.VkId,
                    Artist = Plugin.MBApiInterface.Library_GetFileTag(item.Path, Plugin.MetaDataType.Artist),
                    Title = Plugin.MBApiInterface.Library_GetFileTag(item.Path, Plugin.MetaDataType.TrackTitle)
                });
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

            int lastIndex = MBAudioList.Count > 0 ? MBAudioList[0].Index : 0;
            var items = VkAudioList.Where(vm => vm.IsSelected).Reverse().Select((vm, i) => 
            {
                int index = lastIndex + i + 1;
                Plugin.CalcIndices(index, out int i1, out int i2);
                string i1Str = i1.ToString().PadLeft(2, '0');
                string i2Str = i2.ToString().PadLeft(2, '0');

                // TODO add index to tag replacer
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
            }).ToArray();

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
                Plugin.SetVkId(item.FilePath, item.VM.Id, false);
                Plugin.SetIndex(item.FilePath, item.Index, false);
                Plugin.SetIndex1(item.FilePath, i1, false);
                Plugin.SetIndex2(item.FilePath, i2, false);
                Plugin.MBApiInterface.Library_SetFileTag(item.FilePath, Plugin.MetaDataType.Artist, item.VM.Artist);
                Plugin.MBApiInterface.Library_SetFileTag(item.FilePath, Plugin.MetaDataType.TrackTitle, item.VM.Title);
                Plugin.MBApiInterface.Library_CommitTagsToFile(item.FilePath);
            }

            RefreshMBAudioList();

            foreach (var vkAudio in VkAudioList)
                vkAudio.IsSelected = false;
        }

    }
}
