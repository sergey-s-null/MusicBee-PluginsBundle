using PropertyChanged;

namespace Module.VkAudioDownloader.GUI.AuthDialog
{
    [AddINotifyPropertyChangedInterface]
    public class AuthDialogVM
    {
        public string Login { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
