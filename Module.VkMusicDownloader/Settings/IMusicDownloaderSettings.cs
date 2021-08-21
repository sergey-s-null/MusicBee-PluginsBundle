using Root.Abstractions;

namespace Module.VkMusicDownloader.Settings
{
    public interface IMusicDownloaderSettings : ISettings
    {
        string DownloadDirTemplate { get; set; }
        string FileNameTemplate { get; set; }
        string AccessToken { get; set; }
    }
}