using System;
using System.IO;

namespace VkMusicDownloader.GUI.MainWindow.AddingIncoming
{
    public class IncomingAudioVM : BaseViewModel
    {
        public event EventHandler OnAddToMBLibrary;
        
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

        private RelayCommand _addToMBLibraryCmd;
        public RelayCommand AddToMBLibraryCmd
            => _addToMBLibraryCmd ??= new RelayCommand(_ => AddToMBLibrary());
        
        #endregion

        private void AddToMBLibrary()
        {
            OnAddToMBLibrary?.Invoke(this, EventArgs.Empty);
        }
    }
}
