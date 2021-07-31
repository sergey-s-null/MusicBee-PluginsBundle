using Root.MVVM;
using VkMusicDownloader.GUI.MusicDownloaderWindow.AddingIncoming;
using VkMusicDownloader.GUI.MusicDownloaderWindow.AddingVk;

#pragma warning disable CS4014

namespace VkMusicDownloader.GUI.MusicDownloaderWindow
{
    public class MusicDownloaderWindowVM : BaseViewModel
    {
        #region Bindings

        private readonly AddingVkVM _addingVkVM;
        public AddingVkVM AddingVkVM => _addingVkVM;

        private readonly AddingIncomingVM _addingIncomingVM;
        public AddingIncomingVM AddingIncomingVM => _addingIncomingVM;
        
        #endregion

        public MusicDownloaderWindowVM(
            AddingVkVM addingVkVM,
            AddingIncomingVM addingIncomingVM)
        {
            _addingVkVM = addingVkVM;
            _addingIncomingVM = addingIncomingVM;
        }
    }
}
