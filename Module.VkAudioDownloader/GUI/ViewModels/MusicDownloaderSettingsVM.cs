using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.WindowsAPICodePack.Dialogs;
using Module.VkAudioDownloader.GUI.AbstractViewModels;
using Module.VkAudioDownloader.Settings;
using Module.VkAudioDownloader.TagReplacer;
using PropertyChanged;
using Root.GUI.ViewModels;
using Root.MVVM;

namespace Module.VkAudioDownloader.GUI.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MusicDownloaderSettingsVM : BaseSettingsVM, IMusicDownloaderSettingsVM
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

        // TODO проверить работает ли авто свойство
        public string AvailableTags { get; }
        public string DownloadDirCheck { get; private set; }
        public string FileNameCheck { get; private set; }

        public ICommand ChangeDownloadDirCmd
            => _changeDownloadDirCmd ??= new RelayCommand(arg =>
            {
                if (arg is Window ownerWindow)
                    ChangeDownloadDirectory(ownerWindow);
            });

        private ICommand? _changeDownloadDirCmd;

        private readonly IMusicDownloaderSettings _musicDownloaderSettings;

        private readonly MBTagReplacer _replacer = new();

        public MusicDownloaderSettingsVM(IMusicDownloaderSettings musicDownloaderSettings)
            : base(musicDownloaderSettings)
        {
            _musicDownloaderSettings = musicDownloaderSettings;

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

        protected override void SetSettingsFromInnerServiceToViewModel()
        {
            DownloadDirTemplate = _musicDownloaderSettings.DownloadDirTemplate;
            FileNameTemplate = _musicDownloaderSettings.FileNameTemplate;
        }

        protected override void SetSettingsFromViewModelToInnerService()
        {
            _musicDownloaderSettings.DownloadDirTemplate = DownloadDirTemplate;
            _musicDownloaderSettings.FileNameTemplate = FileNameTemplate;
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