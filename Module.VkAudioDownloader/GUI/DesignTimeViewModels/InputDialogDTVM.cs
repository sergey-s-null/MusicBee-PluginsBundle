using Module.VkAudioDownloader.GUI.AbstractViewModels;

namespace Module.VkAudioDownloader.GUI.DesignTimeViewModels
{
    public sealed class InputDialogDTVM : IInputDialogVM
    {
        public string TitleText { get; set; } = "Input some text to text box below!";
        public string InputText { get; set; } = "Some text in text box";
    }
}