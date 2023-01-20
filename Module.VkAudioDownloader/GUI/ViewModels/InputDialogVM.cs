using Module.VkAudioDownloader.GUI.AbstractViewModels;
using PropertyChanged;

namespace Module.VkAudioDownloader.GUI.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    internal sealed class InputDialogVM : IInputDialogVM
    {
        public string TitleText { get; set; } = "";
        public string InputText { get; set; } = "";
    }
}
