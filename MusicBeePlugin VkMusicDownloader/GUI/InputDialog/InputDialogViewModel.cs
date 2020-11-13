using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicBeePlugin_VkMusicDownloader
{
    class InputDialogViewModel : BaseViewModel
    {
        private string _titleText = "";
        public string TitleText
        {
            get => _titleText;
            set
            {
                _titleText = value;
                NotifyPropertyChanged(nameof(TitleText));
            }
        }

        private string _inputText = "";
        public string InputText
        {
            get => _inputText;
            set
            {
                _inputText = value;
                NotifyPropertyChanged(nameof(InputText));
            }
        }
    }
}
