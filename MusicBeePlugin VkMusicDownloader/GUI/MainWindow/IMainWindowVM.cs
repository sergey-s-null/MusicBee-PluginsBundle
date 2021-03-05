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

        public bool IsLoadingVkAudio { get; set; }

        public bool IsRefreshing { get; set; }

        public RelayCommand RefreshCmd { get; }

        public RelayCommand ApplyCheckStateToSelectedCmd { get; }

        public RelayCommand ApplyCommand { get; }

        public bool IsApplying { get; set; }

        public ObservableCollection<BaseAudioVM> Audios { get; }

        #endregion

        public VkNet.VkApi VkApi { get; set; }

        
    }
}
