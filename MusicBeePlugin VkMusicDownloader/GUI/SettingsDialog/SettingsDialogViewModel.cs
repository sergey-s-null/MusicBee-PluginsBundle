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
        #region Bindings

        private string _availableTags;
        public string AvailableTags
        {
            get => _availableTags;
            set
            {
                _availableTags = value;
                NotifyPropChanged(nameof(AvailableTags));
            }
        }

        private string _fileNameTemplate = Plugin.Settings.FileNameTemplate;
        public string FileNameTemplate
        {
            get => _fileNameTemplate;
            set
            {
                _fileNameTemplate = value;
                FileNameCheck = _replacer.Prepare(value);
                NotifyPropChanged(nameof(FileNameTemplate));
            }
        }

        private string _fileNameCheck;
        public string FileNameCheck
        {
            get => _fileNameCheck;
            set
            {
                _fileNameCheck = value;
                NotifyPropChanged(nameof(FileNameCheck));
            }
        }
        
        private string _downloadDirTemplate = Plugin.Settings.DownloadDirTemplate;
        public string DownloadDirTemplate
        {
            get => _downloadDirTemplate;
            set
            {
                _downloadDirTemplate = value;
                DownloadDirCheck = _replacer.Prepare(value);
                NotifyPropChanged(nameof(DownloadDirTemplate));
            }
        }

        private string _downloadDirCheck;
        public string DownloadDirCheck
        {
            get => _downloadDirCheck;
            set
            {
                _downloadDirCheck = value;
                NotifyPropChanged(nameof(DownloadDirCheck));
            }
        }

        private string _ownerId = Plugin.Settings.OwnerId;
        public string OwnerId
        {
            get => _ownerId;
            set
            {
                _ownerId = value;
                NotifyPropChanged(nameof(OwnerId));
            }
        }

        private RelayCommand _changeDownloadDirCmd;
        public RelayCommand ChangeDownloadDirCmd
            => _changeDownloadDirCmd ?? (_changeDownloadDirCmd = new RelayCommand(arg =>
            {
                if (arg is Window ownerWindow)
                    ChangeDownloadDirectory(ownerWindow);
            }));
        
        #endregion

        private MBTagReplacer _replacer = new MBTagReplacer();

        public SettingsDialogViewModel()
        {
            char ob = MBTagReplacer.OpenBracket;
            char cb = MBTagReplacer.CloseBracket;
            _availableTags = MBTagReplacer.AvailableTags
                .Select(tag => $"{ob}{tag}{cb}")
                .Aggregate((a, b) => $"{a}; {b}");

            _replacer.SetReplaceValue(MBTagReplacer.Tag.Index1, "Index1");
            _replacer.SetReplaceValue(MBTagReplacer.Tag.Index2, "Index2");
            _replacer.SetReplaceValue(MBTagReplacer.Tag.Artist, "Artist");
            _replacer.SetReplaceValue(MBTagReplacer.Tag.Title, "Title");

            _fileNameCheck = _replacer.Prepare(_fileNameTemplate);
            _downloadDirCheck = _replacer.Prepare(_downloadDirTemplate);
        }

        public bool SaveChanges()
        {
            Plugin.Settings.DownloadDirTemplate = DownloadDirTemplate;
            Plugin.Settings.OwnerId = OwnerId;
            Plugin.Settings.FileNameTemplate = FileNameTemplate;
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
                dialog.DefaultDirectory = DownloadDirTemplate;
                if (dialog.ShowDialog(ownerWindow) == CommonFileDialogResult.Ok)
                    DownloadDirTemplate = dialog.FileName;
            }
        }
    }
}
