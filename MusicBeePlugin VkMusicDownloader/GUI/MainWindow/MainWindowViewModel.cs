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
        private RelayCommand _autoCheckCommand;
        public RelayCommand AutoCheckCommand
            => _autoCheckCommand ?? (_autoCheckCommand = new RelayCommand(_ => AutoCheck()));

        private RelayCommand _refreshVkAudioCommand;
        public RelayCommand RefreshVkAudioCommand
            => _refreshVkAudioCommand ?? (_refreshVkAudioCommand = new RelayCommand(_ => RefreshVkAudioList()));

        private ObservableCollection<VkAudioViewModel> _vkAudioList;
        public ObservableCollection<VkAudioViewModel> VkAudioList
            => _vkAudioList ?? (_vkAudioList = new ObservableCollection<VkAudioViewModel>());

        private bool _isLoadingVkAudio = false;
        public bool IsLoadingVkAudio
        {
            get => _isLoadingVkAudio;
            set
            {
                _isLoadingVkAudio = value;
                NotifyPropertyChanged(nameof(IsLoadingVkAudio));
            }
        }
        
        private RelayCommand _next10AudiosCommand;
        public RelayCommand Next10AudiosCommand
            => _next10AudiosCommand ?? (_next10AudiosCommand = new RelayCommand(_ => Next10AudiosAsync()));

        private ObservableCollection<MBAudioViewModel> _mbAudioList;
        public ObservableCollection<MBAudioViewModel> MBAudioList
            => _mbAudioList ?? (_mbAudioList = new ObservableCollection<MBAudioViewModel>());

        private RelayCommand _applyCheckStateToSelectedCommand;
        public RelayCommand ApplyCheckStateToSelectedCommand
            => _applyCheckStateToSelectedCommand ?? (_applyCheckStateToSelectedCommand = new RelayCommand(arg =>
            {
                object[] arr = arg as object[];
                if (arr is null || arr.Length != 2)
                    return;
                VkAudioViewModel viewModel = arr[0] as VkAudioViewModel;
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
                NotifyPropertyChanged(nameof(IsApplying));
            }
        }

        private RelayCommand _applyCommand;
        public RelayCommand ApplyCommand
            => _applyCommand ?? (_applyCommand = new RelayCommand(_ => Apply()));

        private VkAudioApi _vkApi;
        private int _audioDataShift = 0;

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
                    if (MBAudioList[i].Artist == VkAudioList[j].Artist && MBAudioList[i].Title == VkAudioList[j].Title)
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

        private void ApplyCheckStateToSelected(VkAudioViewModel triggeredViewModel, IList<object> selectedViewModels)
        {
            if (!selectedViewModels.Contains(triggeredViewModel))
                return;

            foreach (object item in selectedViewModels)
            {
                if (item is VkAudioViewModel selected && selected != triggeredViewModel)
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
                    var viewModel = new VkAudioViewModel(data.Url)
                    {
                        Id = data.Id,
                        Artist = data.Artist,
                        Title = data.Title,
                        Duration = data.Duration
                    };
                    VkAudioList.Add(viewModel);
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
                bool isReceivedTags = Plugin.MBApiInterface.Library_GetFileTags(path, new Plugin.MetaDataType[] {
                    Plugin.MetaDataType.Custom1,
                    Plugin.MetaDataType.Custom2
                }, out string[] values);

                if (!isReceivedTags || !int.TryParse(values[0], out int i1) || !int.TryParse(values[1], out int i2))
                    return null;

                return new
                {
                    Index1 = i1,
                    Index2 = i2,
                    Path = path
                };
            }).ToList();

            list.RemoveAll(a => a is null);

            list.Sort((a, b) =>
            {
                int compareRes = b.Index1.CompareTo(a.Index1);
                if (compareRes == 0)
                    return b.Index2.CompareTo(a.Index2);
                else
                    return compareRes;
            });

            if (list.Count > 20)
                list.RemoveRange(20, list.Count - 20);

            foreach (var item in list)
            {
                MBAudioList.Add(new MBAudioViewModel
                {
                    Index1 = item.Index1,
                    Index2 = item.Index2,
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
            string downloadDir = Plugin.Settings.DownloadDirectory;
            if (downloadDir.Length == 0)
            {
                MessageBox.Show("Download directory is empty. Set it in settings.");
                return;
            }

            List<VkAudioViewModel> viewModels = VkAudioList.ToList();
            viewModels.RemoveAll(vm => !vm.IsSelected);

            int audiosPerBlock = 20;// TODO move
            int lastIndex1 = MBAudioList.Count > 0 ? MBAudioList[0].Index1 : 1;
            int lastIndex2 = MBAudioList.Count > 0 ? MBAudioList[0].Index2 : 1;
            var items = viewModels.Select((vm, i) =>
            {
                int i2 = lastIndex2 + viewModels.Count - i - 1;
                int i1 = lastIndex1 + i2 / audiosPerBlock;
                i2 = i2 % audiosPerBlock + 1;
                string i1Str = i1.ToString().PadLeft(2, '0');
                string i2Str = i2.ToString().PadLeft(2, '0');

                string filePath = Path.Combine(downloadDir, $"[{i1Str}-{i2Str}] {vm.Artist} - {vm.Title}.mp3");
                WebClient webClient = new WebClient();
                Task task = webClient.DownloadFileTaskAsync(vm.Url, filePath);

                return new
                {
                    WebClient = webClient,
                    FilePath = filePath,
                    Task = task,
                    Index1 = i1,
                    Index2 = i2
                };
            }).ToArray();

            bool isAllNice;
            try
            {
                foreach (var item in items)
                {
                    await item.Task;
                    item.WebClient.Dispose();
                }
                isAllNice = true;
            }
            catch
            {
                isAllNice = false;
            }

            if (isAllNice)
            {
                for (int i = 0; i < viewModels.Count; ++i)
                {
                    string addedPath = AddToMBLibrary(items[i].FilePath, items[i].Index1, items[i].Index2, 
                        viewModels[i].Artist, viewModels[i].Title);
                }
            }
            else
            {
                foreach (var item in items)
                    if (File.Exists(item.FilePath))
                        File.Delete(item.FilePath);
            }

            RefreshMBAudioList();

            foreach (var vkAudio in VkAudioList)
                vkAudio.IsSelected = false;
        }

        private string AddToMBLibrary(string filePath, int index1, int index2, string artist, string title)
        {
            string addedPath = Plugin.MBApiInterface.Library_AddFileToLibrary(filePath, Plugin.LibraryCategory.Music);

            if (addedPath is object && addedPath.Length != 0)
            {
                Plugin.MBApiInterface.Library_SetFileTag(addedPath, Plugin.MetaDataType.Artist, artist);
                Plugin.MBApiInterface.Library_SetFileTag(addedPath, Plugin.MetaDataType.TrackTitle, title);

                Plugin.MBApiInterface.Library_SetFileTag(addedPath, Plugin.MetaDataType.Custom1, index1.ToString().PadLeft(2, '0'));
                Plugin.MBApiInterface.Library_SetFileTag(addedPath, Plugin.MetaDataType.Custom2, index2.ToString().PadLeft(2, '0'));

                Plugin.MBApiInterface.Library_CommitTagsToFile(addedPath);
                return addedPath;
            }
            else
            {
                return "";
            }
        }

    }
}
