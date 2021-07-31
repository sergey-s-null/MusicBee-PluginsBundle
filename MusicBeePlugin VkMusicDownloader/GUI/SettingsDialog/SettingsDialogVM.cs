using System.Linq;
using System.Windows;
using Microsoft.WindowsAPICodePack.Dialogs;
using Root.MVVM;
using VkMusicDownloader.Settings;
using VkMusicDownloader.TagReplacer;

namespace VkMusicDownloader.GUI.SettingsDialog
{
    public class SettingsDialogVM : BaseViewModel
    {
        #region Bindings

        // TODO проверить работает ли авто свойство
        private string _availableTags;
        public string AvailableTags => _availableTags;
        
        public string FileNameTemplate
        {
            get => _settings.FileNameTemplate;
            set
            {
                _settings.FileNameTemplate = value;
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
        
        public string DownloadDirTemplate
        {
            get => _settings.DownloadDirTemplate;
            set
            {
                _settings.DownloadDirTemplate = value;
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

        public string AccessToken
        {
            get => _settings.AccessToken;
            set
            {
                _settings.AccessToken = value;
                NotifyPropChanged(nameof(AccessToken));
            }
        }

        private bool _isEditAccessToken = false;
        public bool IsEditAccessToken
        {
            get => _isEditAccessToken;
            set
            {
                _isEditAccessToken = value;
                NotifyPropChanged(nameof(IsEditAccessToken));
            }
        }

        private RelayCommand _changeDownloadDirCmd;
        public RelayCommand ChangeDownloadDirCmd
            => _changeDownloadDirCmd ??= new RelayCommand(arg =>
            {
                if (arg is Window ownerWindow)
                    ChangeDownloadDirectory(ownerWindow);
            });
        
        #endregion

        private readonly IMusicDownloaderSettings _settings;

        private readonly MBTagReplacer _replacer = new();

        public SettingsDialogVM(IMusicDownloaderSettings settings)
        {
            _settings = settings;
            
            var ob = MBTagReplacer.OpenBracket;
            var cb = MBTagReplacer.CloseBracket;
            _availableTags = MBTagReplacer.AvailableTags
                .Select(tag => $"{ob}{tag}{cb}")
                .Aggregate((a, b) => $"{a}; {b}");

            _replacer.SetReplaceValue(MBTagReplacer.Tag.Index1, "Index1");
            _replacer.SetReplaceValue(MBTagReplacer.Tag.Index2, "Index2");
            _replacer.SetReplaceValue(MBTagReplacer.Tag.Artist, "Artist");
            _replacer.SetReplaceValue(MBTagReplacer.Tag.Title, "Title");

            _fileNameCheck = _replacer.Prepare(FileNameTemplate);
            _downloadDirCheck = _replacer.Prepare(DownloadDirTemplate);
        }

        public bool SaveChanges()
        {
            if (!_settings.Save())
            {
                MessageBox.Show("Error save settings.");
                return false;
            }

            return true;
        }

        private void ChangeDownloadDirectory(Window ownerWindow)
        {
            using var dialog = new CommonOpenFileDialog
            {
                IsFolderPicker = true,
                DefaultDirectory = DownloadDirTemplate
            };
            
            if (dialog.ShowDialog(ownerWindow) == CommonFileDialogResult.Ok)
                DownloadDirTemplate = dialog.FileName;
        }
    }
}
