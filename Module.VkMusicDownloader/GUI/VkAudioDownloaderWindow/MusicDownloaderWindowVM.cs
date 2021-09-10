using Module.VkMusicDownloader.GUI.VkAudioDownloaderWindow.AddingVk;

namespace Module.VkMusicDownloader.GUI.VkAudioDownloaderWindow
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
