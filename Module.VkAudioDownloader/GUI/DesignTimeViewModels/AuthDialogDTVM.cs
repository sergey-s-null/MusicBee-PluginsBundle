using Module.VkAudioDownloader.GUI.AbstractViewModels;

namespace Module.VkAudioDownloader.GUI.DesignTimeViewModels
{
    public class AuthDialogDTVM : IAuthDialogVM
    {
        public string Login { get; set; } = "SomeLogin123";
        public string Password { get; set; } = "123passwordlkj";
    }
}