using Root.MVVM;

namespace Module.VkMusicDownloader.GUI.InputDialog
{
    class InputDialogVM : BaseViewModel
    {
        private string _titleText = "";
        public string TitleText
        {
            get => _titleText;
            set
            {
                _titleText = value;
                NotifyPropChanged(nameof(TitleText));
            }
        }

        private string _inputText = "";
        public string InputText
        {
            get => _inputText;
            set
            {
                _inputText = value;
                NotifyPropChanged(nameof(InputText));
            }
        }
    }
}
