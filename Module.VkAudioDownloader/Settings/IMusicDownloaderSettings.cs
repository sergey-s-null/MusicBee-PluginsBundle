namespace Module.VkAudioDownloader.Settings
{
    public interface IMusicDownloaderSettings
    {
        string DownloadDirTemplate { get; set; }
        string FileNameTemplate { get; set; }

        void Load();
        void Save();
    }
}