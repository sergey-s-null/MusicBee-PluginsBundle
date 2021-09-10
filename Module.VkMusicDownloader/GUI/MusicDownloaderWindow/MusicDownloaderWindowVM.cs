using Module.VkMusicDownloader.GUI.MusicDownloaderWindow.AddingVk;

namespace Module.VkMusicDownloader.GUI.MusicDownloaderWindow
{
    public class MusicDownloaderWindowVM
    {
        public AddingVkVM AddingVkVM { get; }
        
        public MusicDownloaderWindowVM(
            AddingVkVM addingVkVM)
        {
            AddingVkVM = addingVkVM;
        }
    }
}
