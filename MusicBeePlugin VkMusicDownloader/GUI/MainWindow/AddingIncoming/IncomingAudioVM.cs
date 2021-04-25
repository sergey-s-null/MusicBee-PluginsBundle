using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VkMusicDownloader.GUI
{
    public class IncomingAudioVM : BaseViewModel
    {
        #region Bindings

        private string _filePath = "";
        public string FilePath
        {
            get => _filePath;
            set
            {
                _filePath = value;
                NotifyPropChanged(nameof(FilePath), nameof(FileName));
            }
        }

        public string FileName => Path.GetFileName(FilePath);

        private string _artist = "";
        public string Artist
        {
            get => _artist;
            set
            {
                _artist = value;
                NotifyPropChanged(nameof(Artist));
            }
        }

        private string _title = "";
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                NotifyPropChanged(nameof(Title));
            }
        }

        #endregion
    }
}
