using System;
using System.Windows.Input;
using Module.VkAudioDownloader.GUI.AbstractViewModels;
using Root.MVVM;

namespace Module.VkAudioDownloader.GUI.DesignTimeViewModels
{
    public class MusicDownloaderSettingsDTVM : IMusicDownloaderSettingsVM
    {
        public bool Loaded => true;
        public string LoadingErrorMessage => "(no error)";

        public string DownloadDirTemplate { get; set; } = @"D:\Path\To\Directory\<i1>\<i2>";
        public string FileNameTemplate { get; set; } = "<artist> - <title>";
        public string AvailableTags => "<i1>; <i2>; <artist>; <title>";
        public string DownloadDirCheck => @"D:\Path\To\Directory\Index1\Index2";
        public string FileNameCheck => "Artist - Title";

        public ICommand ChangeDownloadDirCmd =>
            new RelayCommand(_ => throw new NotSupportedException());

        public ICommand ReloadCmd { get; } = new RelayCommand(_ => { });

        public void Load()
        {
            throw new NotSupportedException();
        }

        public bool Save()
        {
            throw new NotSupportedException();
        }
    }
}