using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VkMusicDownloader.GUI
{
    public interface IMainWindowVM
    {

        #region Bindings

        //private RelayCommand _autoCheckCommand;
        //public RelayCommand AutoCheckCommand
        //    => _autoCheckCommand ?? (_autoCheckCommand = new RelayCommand(_ => AutoCheck()));

        //private RelayCommand _refreshVkAudioCommand;
        //public RelayCommand RefreshVkAudioCommand
        //    => _refreshVkAudioCommand ?? (_refreshVkAudioCommand = new RelayCommand(_ => RefreshVkAudioList()));

        //private ObservableCollection<VkAudioVM> _vkAudioList;
        //public ObservableCollection<VkAudioVM> VkAudioList
        //    => _vkAudioList ?? (_vkAudioList = new ObservableCollection<VkAudioVM>());

        public bool IsLoadingVkAudio { get; set; }

        //private RelayCommand _next10AudiosCommand;
        //public RelayCommand Next10AudiosCommand
        //    => _next10AudiosCommand ?? (_next10AudiosCommand = new RelayCommand(_ => Next10AudiosAsync()));

        //private ObservableCollection<MBAudioVM> _mbAudioList;
        //public ObservableCollection<MBAudioVM> MBAudioList
        //    => _mbAudioList ?? (_mbAudioList = new ObservableCollection<MBAudioVM>());


        //private RelayCommand _refreshMBAudioListCommand;
        //public RelayCommand RefreshMBAudioListCommand
        //    => _refreshMBAudioListCommand ?? (_refreshMBAudioListCommand = new RelayCommand(_ => RefreshMBAudioList()));

        public RelayCommand RefreshCmd { get; }

        public RelayCommand ApplyCheckStateToSelectedCmd { get; }

        public RelayCommand ApplyCommand { get; }

        public bool IsApplying { get; set; }

        public ObservableCollection<BaseAudioVM> Audios { get; }

        #endregion

        public VkNet.VkApi VkApi { get; set; }

        // protected abstract void AutoCheck();

        //protected abstract void RefreshVkAudioList();

        //private void ApplyCheckStateToSelected(VkAudioVM triggeredViewModel, IList<object> selectedViewModels)
        //{
        //    if (!selectedViewModels.Contains(triggeredViewModel))
        //        return;

        //    foreach (object item in selectedViewModels)
        //    {
        //        if (item is VkAudioVM selected && selected != triggeredViewModel)
        //            selected.IsSelected = triggeredViewModel.IsSelected;
        //    }
        //}

        //protected abstract Task Next10AudiosAsync();

        //protected abstract void RefreshMBAudioList();

        
    }
}
