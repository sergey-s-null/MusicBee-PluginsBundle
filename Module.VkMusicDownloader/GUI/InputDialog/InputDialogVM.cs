using PropertyChanged;

namespace Module.VkMusicDownloader.GUI.InputDialog
{
    [AddINotifyPropertyChangedInterface]
    class InputDialogVM
    {
        public string TitleText { get; set; } = "";
        public string InputText { get; set; } = "";
    }
}
