using PropertyChanged;

namespace Module.VkAudioDownloader.GUI.InputDialog
{
    [AddINotifyPropertyChangedInterface]
    class InputDialogVM
    {
        public string TitleText { get; set; } = "";
        public string InputText { get; set; } = "";
    }
}
