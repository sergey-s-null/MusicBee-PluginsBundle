using System.Windows.Input;
using Module.VkAudioDownloader.Settings;

namespace Module.VkAudioDownloader.GUI.Settings
{
    public interface IMusicDownloaderSettingsVM : IMusicDownloaderSettings
    {
        string AvailableTags { get; }
        string DownloadDirCheck { get; }
        string FileNameCheck { get; }
        
        ICommand ChangeDownloadDirCmd { get; }
    }
}