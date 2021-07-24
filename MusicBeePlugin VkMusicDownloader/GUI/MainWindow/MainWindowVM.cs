using VkMusicDownloader.GUI.MainWindow.AddingIncoming;
using VkMusicDownloader.GUI.MainWindow.AddingVk;

#pragma warning disable CS4014

namespace VkMusicDownloader.GUI.MainWindow
{
    public class MainWindowVM : BaseViewModel
    {
        #region Bindings

        private readonly AddingVkVM _addingVkVM;
        public AddingVkVM AddingVkVM => _addingVkVM;

        private readonly AddingIncomingVM _addingIncomingVM;
        public AddingIncomingVM AddingIncomingVM => _addingIncomingVM;
        
        #endregion

        public MainWindowVM(
            AddingVkVM addingVkVM,
            AddingIncomingVM addingIncomingVM)
        {
            _addingVkVM = addingVkVM;
            _addingIncomingVM = addingIncomingVM;
        }
    }
}
