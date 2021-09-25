using System.Windows;
using Module.InboxAdder.Services;
using Root;

namespace MusicBeePlugin.Services
{
    public class MusicBeeInboxAddService : IMusicBeeInboxAddService
    {
        private readonly MusicBeeApiInterface _mbApi;
        private readonly IInboxAddService _inboxAddService;

        public MusicBeeInboxAddService(
            MusicBeeApiInterface mbApi, 
            IInboxAddService inboxAddService)
        {
            _mbApi = mbApi;
            _inboxAddService = inboxAddService;
        }

        public void AddSelectedFileToLibrary()
        {
            var queryRes = _mbApi.Library_QueryFilesEx.Invoke("domain=SelectedFiles", out var files);

            if (!queryRes || files.Length != 1)
            {
                MessageBox.Show("You must select single item.");
                return;
            }

            _inboxAddService.AddToLibrary(files[0]);

            _mbApi.MB_RefreshPanels();
        }
    }
}