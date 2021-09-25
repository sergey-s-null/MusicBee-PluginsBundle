namespace MusicBeePlugin.Services
{
    public interface IPluginActions
    {
        void SearchArtworks();
        void DownloadVkAudios();
        void AddSelectedFileToLibrary();
        void ExportPlaylists();
        void ExportLibraryData();
    }
}