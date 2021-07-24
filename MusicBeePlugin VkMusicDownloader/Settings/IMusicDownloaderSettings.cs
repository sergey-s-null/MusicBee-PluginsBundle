namespace VkMusicDownloader.Settings
{
    public interface IMusicDownloaderSettings
    {
        bool IsLoaded { get; }
        void Load();
        bool Save();
        
        string DownloadDirTemplate { get; set; }
        string FileNameTemplate { get; set; }
        string AccessToken { get; set; }
    }
}