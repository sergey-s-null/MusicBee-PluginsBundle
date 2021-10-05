namespace MusicBeePlugin.Services
{
    public interface IPluginActions
    {
        void SearchArtworks();
        void DownloadVkAudios();
        void AddSelectedFileToLibrary();
        void RetrieveSelectedFileToInbox();
        void ExportPlaylists();
        void ExportLibraryData();
    }
}