using PropertyChanged;

namespace Module.VkMusicDownloader.GUI.AuthDialog
{
    [AddINotifyPropertyChangedInterface]
    public class AuthDialogVM
    {
        public string Login { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
