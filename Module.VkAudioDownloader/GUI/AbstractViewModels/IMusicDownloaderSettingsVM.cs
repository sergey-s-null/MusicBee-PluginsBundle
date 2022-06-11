using System.Windows.Input;
using Root.GUI.AbstractViewModels;

namespace Module.VkAudioDownloader.GUI.AbstractViewModels
{
    public interface IMusicDownloaderSettingsVM : IBaseSettingsVM
    {
        public string DownloadDirTemplate { get; set; }
        public string FileNameTemplate { get; set; }
        string AvailableTags { get; }
        string DownloadDirCheck { get; }
        string FileNameCheck { get; }

        ICommand ChangeDownloadDirCmd { get; }
    }
}