using Microsoft.WindowsAPICodePack.Dialogs;
using MusicBeePlugin_VkMusicDownloader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MusicBeePlugin.GUI
{
    class SettingsDialogViewModel : BaseViewModel
    {
        private string _downloadDirectory = Plugin.Settings.DownloadDirectory;
        public string DownloadDirectory
        {
            get => _downloadDirectory;
            set
            {
                _downloadDirectory = value;
                NotifyPropertyChanged(nameof(DownloadDirectory));
            }
        }
        
        private string _ownerId = Plugin.Settings.OwnerId;
        public string OwnerId
        {
            get => _ownerId;
            set
            {
                _ownerId = value;
                NotifyPropertyChanged(nameof(OwnerId));
            }
        }

        private RelayCommand _changeDownloadDirCmd;
        public RelayCommand ChangeDownloadDirCmd
            => _changeDownloadDirCmd ?? (_changeDownloadDirCmd = new RelayCommand(arg =>
            {
                if (arg is Window ownerWindow)
                    ChangeDownloadDirectory(ownerWindow);
            }));

        public bool SaveChanges()
        {
            Plugin.Settings.DownloadDirectory = DownloadDirectory;
            Plugin.Settings.OwnerId = OwnerId;
            if (!Plugin.Settings.Save())
            {
                MessageBox.Show("Error save settings.");
                return false;
            }

            return true;
        }

        private void ChangeDownloadDirectory(Window ownerWindow)
        {
            using (var dialog = new CommonOpenFileDialog())
            {
                dialog.IsFolderPicker = true;
                dialog.DefaultDirectory = DownloadDirectory;
                if (dialog.ShowDialog(ownerWindow) == CommonFileDialogResult.Ok)
                    DownloadDirectory = dialog.FileName;
            }
        }
    }
}
