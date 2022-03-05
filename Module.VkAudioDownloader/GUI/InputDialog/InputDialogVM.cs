using PropertyChanged;

namespace Module.VkAudioDownloader.GUI.InputDialog
{
    [AddINotifyPropertyChangedInterface]
    internal class InputDialogVM : IInputDialogVM
    {
        public string TitleText { get; set; } = "";
        public string InputText { get; set; } = "";
    }
}
