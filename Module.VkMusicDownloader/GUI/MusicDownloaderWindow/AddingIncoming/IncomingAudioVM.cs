using System;
using System.IO;
using PropertyChanged;
using Root.MVVM;

namespace Module.VkMusicDownloader.GUI.MusicDownloaderWindow.AddingIncoming
{
    [AddINotifyPropertyChangedInterface]
    public class IncomingAudioVM
    {
        public event EventHandler OnAddToMBLibrary;
        
        #region Bindings

        public string FilePath { get; set; } = "";
        public string FileName => Path.GetFileName(FilePath);
        public string Artist { get; set; } = "";
        public string Title { get; set; } = "";

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
