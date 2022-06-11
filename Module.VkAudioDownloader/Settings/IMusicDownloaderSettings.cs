namespace Module.VkAudioDownloader.Settings
{
    public interface IMusicDownloaderSettings
    {
        string DownloadDirTemplate { get; set; }
        string FileNameTemplate { get; set; }

        bool Load();
        bool Save();
    }
}