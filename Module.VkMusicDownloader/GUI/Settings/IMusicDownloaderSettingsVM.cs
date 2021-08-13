using System.Windows.Input;
using Module.VkMusicDownloader.Settings;

namespace Module.VkMusicDownloader.GUI.Settings
{
    public interface IMusicDownloaderSettingsVM : IMusicDownloaderSettings
    {
        string AvailableTags { get; }
        string DownloadDirCheck { get; }
        string FileNameCheck { get; }
        
        ICommand ChangeDownloadDirCmd { get; }
    }
}