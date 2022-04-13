using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.WindowsAPICodePack.Dialogs;
using Module.VkAudioDownloader.GUI.AbstractViewModels;
using Module.VkAudioDownloader.Settings;
using Module.VkAudioDownloader.TagReplacer;
using PropertyChanged;
using Root.MVVM;

namespace Module.VkAudioDownloader.GUI.ViewModels
{
    // TODO turn on Fody
    [AddINotifyPropertyChangedInterface]
    public class MusicDownloaderSettingsVM : IMusicDownloaderSettingsVM
    {
        [OnChangedMethod(nameof(OnDownloadDirTemplateChanged))]
        public string DownloadDirTemplate { get; set; } = "";
        public void OnDownloadDirTemplateChanged()
        {
            DownloadDirCheck = _replacer.Prepare(DownloadDirTemplate);
        }
        
        [OnChangedMethod(nameof(OnFileNameTemplateChanged))]
        public string FileNameTemplate { get; set; } = "";
        public void OnFileNameTemplateChanged()
        {
            FileNameCheck = _replacer.Prepare(FileNameTemplate);
        }
        
        public string AccessToken { get; set; } = "";
        
        // TODO проверить работает ли авто свойство
        public string AvailableTags { get; }
        public string DownloadDirCheck { get; private set; }
        public string FileNameCheck { get; private set; }
        
        private ICommand? _changeDownloadDirCmd;
        public ICommand ChangeDownloadDirCmd
            => _changeDownloadDirCmd ??= new RelayCommand(arg =>
            {
                if (arg is Window ownerWindow)
                    ChangeDownloadDirectory(ownerWindow);
            });
        
        public bool IsLoaded => _settings.IsLoaded;

        private readonly IMusicDownloaderSettings _settings;

        private readonly MBTagReplacer _replacer = new();

        public MusicDownloaderSettingsVM(IMusicDownloaderSettings settings)
        {
            _settings = settings;
            
            var openBracket = MBTagReplacer.OpenBracket;
            var closeBracket = MBTagReplacer.CloseBracket;
            AvailableTags = MBTagReplacer.AvailableTags
                .Select(tag => $"{openBracket}{tag}{closeBracket}")
                .Aggregate((a, b) => $"{a}; {b}");

            _replacer.SetReplaceValue(MBTagReplacer.Tag.Index1, "Index1");
            _replacer.SetReplaceValue(MBTagReplacer.Tag.Index2, "Index2");
            _replacer.SetReplaceValue(MBTagReplacer.Tag.Artist, "Artist");
            _replacer.SetReplaceValue(MBTagReplacer.Tag.Title, "Title");

            FileNameCheck = _replacer.Prepare(FileNameTemplate);
            DownloadDirCheck = _replacer.Prepare(DownloadDirTemplate);
        }
        
        public bool Load()
        {
            if (!_settings.Load())
            {
                return false;
            }

            Reset();
            return true;
        }

        public bool Save()
        {
            _settings.DownloadDirTemplate = DownloadDirTemplate;
            _settings.FileNameTemplate = FileNameTemplate;
            _settings.AccessToken = AccessToken;
            
            if (_settings.Save()) return true;
            
            // TODO вероятно здесь не нужен диалог
            MessageBox.Show("Error save settings.");
            
            return false;
        }

        public void Reset()
        {
            DownloadDirTemplate = _settings.DownloadDirTemplate;
            FileNameTemplate = _settings.FileNameTemplate;
            AccessToken = _settings.AccessToken;
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
