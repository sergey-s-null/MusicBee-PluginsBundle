namespace Plugin.Main.Services
{
    public interface IPluginActions
    {
        void SearchArtworks();
        void DownloadVkAudios();
        void CompareVkAndLocalAudios();
        void AddSelectedFileToLibrary();
        void RetrieveSelectedFileToInbox();
        void ExportPlaylists();
        void ExportLibraryData();
    }
}