using System.Windows.Input;
using VkMusicDownloader.Settings;

namespace VkMusicDownloader.GUI.Settings
{
    public interface IMusicDownloaderSettingsVM : IMusicDownloaderSettings
    {
        string AvailableTags { get; }
        string DownloadDirCheck { get; }
        string FileNameCheck { get; }
        
        ICommand ChangeDownloadDirCmd { get; }
    }
}