using Module.VkAudioDownloader.GUI.AbstractViewModels;
using PropertyChanged;

namespace Module.VkAudioDownloader.GUI.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class AuthDialogVM : IAuthDialogVM
    {
        public string Login { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
