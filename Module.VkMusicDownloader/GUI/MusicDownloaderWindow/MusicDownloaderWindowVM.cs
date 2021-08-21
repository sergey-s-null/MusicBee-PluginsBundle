using Module.VkMusicDownloader.GUI.MusicDownloaderWindow.AddingIncoming;
using Module.VkMusicDownloader.GUI.MusicDownloaderWindow.AddingVk;

namespace Module.VkMusicDownloader.GUI.MusicDownloaderWindow
{
    public class MusicDownloaderWindowVM
    {
        public AddingVkVM AddingVkVM { get; }
        public AddingIncomingVM AddingIncomingVM { get; }
        
        public MusicDownloaderWindowVM(
            AddingVkVM addingVkVM,
            AddingIncomingVM addingIncomingVM)
        {
            AddingVkVM = addingVkVM;
            AddingIncomingVM = addingIncomingVM;
        }
    }
}
