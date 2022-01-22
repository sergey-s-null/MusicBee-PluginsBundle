using System;
using System.Linq;
using System.Windows;
using Microsoft.WindowsAPICodePack.Dialogs;
using Module.ArtworksSearcher.Factories;
using Module.DataExporter.Exceptions;
using Module.DataExporter.Services;
using Module.InboxAdder.Factories;
using Module.InboxAdder.Services;
using Module.PlaylistsExporter.Services;
using Module.VkAudioDownloader.Factories;
using Root;
using Root.Helpers;

namespace MusicBeePlugin.Services
{
    public class PluginActions : IPluginActions
    {
        private readonly MusicBeeApiInterface _mbApi;
        private readonly IDataExportService _dataExportService;
        private readonly IPlaylistsExportService _playlistsExportService;
        private readonly IInboxAddService _inboxAddService;
        private readonly ITagsCopyService _tagsCopyService;
        private readonly ISearchWindowFactory _searchWindowFactory;
        private readonly IVkAudioDownloaderWindowFactory _vkAudioDownloaderWindowFactory;
        private readonly IFileByIndexSelectDialogFactory _fileByIndexSelectDialogFactory;

        public PluginActions(
            MusicBeeApiInterface mbApi, 
            IDataExportService dataExportService, 
            IPlaylistsExportService playlistsExportService, 
            IInboxAddService inboxAddService, 
            ITagsCopyService tagsCopyService,
            ISearchWindowFactory searchWindowFactory, 
            IVkAudioDownloaderWindowFactory vkAudioDownloaderWindowFactory,
            IFileByIndexSelectDialogFactory fileByIndexSelectDialogFactory)
        {
            _mbApi = mbApi;
            _dataExportService = dataExportService;
            _playlistsExportService = playlistsExportService;
            _inboxAddService = inboxAddService;
            _tagsCopyService = tagsCopyService;
            _searchWindowFactory = searchWindowFactory;
            _vkAudioDownloaderWindowFactory = vkAudioDownloaderWindowFactory;
            _fileByIndexSelectDialogFactory = fileByIndexSelectDialogFactory;
        }

        public void SearchArtworks()
        {
            if (!TryGetSingleSelectedFile(out var selectedFilePath))
            {
                MessageBox.Show("You must select single item.");
                return;
            }

            var artist = _mbApi.Library_GetFileTag.Invoke(selectedFilePath, MetaDataType.Artist);
            var title = _mbApi.Library_GetFileTag.Invoke(selectedFilePath, MetaDataType.TrackTitle);

            var searchWindow = _searchWindowFactory.Create();
            if (searchWindow.ShowDialog(artist, title, out var imageData))
            {
                if (!_mbApi.Library_SetArtworkEx.Invoke(selectedFilePath, 0, imageData))
                {
                    MessageBox.Show("Обложка не была сохранена.", "Ошибка");
                }
            }
        }

        public void DownloadVkAudios()
        {
            _vkAudioDownloaderWindowFactory
                .Create()
                .ShowDialog();
        }

        public void AddSelectedFileToLibrary()
        {
            if (!TryGetSingleSelectedFile(out var selectedFilePath))
            {
                MessageBox.Show("You must select single item.");
                return;
            }

            _inboxAddService.AddToLibrary(selectedFilePath);

            _mbApi.MB_RefreshPanels();
        }

        public void ReplaceSelectedFileByIndex()
        {
            if (!TryGetSingleSelectedFile(out var inboxFile))
            {
                MessageBox.Show("You must select single item.");
                return;
            }
            
            var dialog = _fileByIndexSelectDialogFactory.Create();
            if (!dialog.ShowDialog(out var fileToDelete))
            {
                return;
            }

            var continueResponse = MessageBox.Show("Replace state:\n" +
                            $"File will be deleted: {fileToDelete};\n" +
                            $"File will be added: {inboxFile}.\n" +
                            $"Continue?",
                "Continue?",
                MessageBoxButton.YesNo);
            if (continueResponse != MessageBoxResult.Yes)
            {
                return;
            }

            // if (!_tagsCopyService.CopyTags(fileToDelete, inboxFile))
            // {
            //     MessageBox.Show("Error on copy tags.", "Error");
            //     return;
            // }

            var startRes = _mbApi.Sync_FileDeleteStart(fileToDelete);
            MessageBox.Show($"Start res: {startRes}");

            _mbApi.Sync_FileDeleteEnd(fileToDelete, true, "Error message");
            
            MessageBox.Show("Delete done?");
            return;
            
            
            _mbApi.Sync_FileDeleteStart(inboxFile);
            // _mbApi.Sync_FileDeleteEnd()
        }

        public void RetrieveSelectedFileToInbox()
        {
            if (!TryGetSingleSelectedFile(out var selectedFilePath))
            {
                MessageBox.Show("You must select single item.");
                return;
            }
            
            _inboxAddService.RetrieveToInbox(selectedFilePath);

            _mbApi.MB_RefreshPanels();
        }
        
        public void ExportPlaylists()
        {
            const int showCount = 10;
            try
            {
                var deletingPlaylistsPaths = _playlistsExportService.GetExistingExportedPlaylists();
                var deletingPlaylists = deletingPlaylistsPaths
                    .Take(showCount)
                    .Select(x => new Uri(x))
                    .ToReadOnlyList();

                var (common, particulars) = UriHelper.SplitOnCommonAndParticulars(deletingPlaylists);
                
                var joined = string.Join(Environment.NewLine, particulars.Select(x => $"\t{x.ToLocalOrBackSlashPath()}"));
                var dialogResult = MessageBox.Show("These files will be deleted before export:\n" +
                                                   $"{common.ToLocalOrBackSlashPath()}\n" +
                                                   $"{joined}\n" +
                                                   (deletingPlaylistsPaths.Count > showCount 
                                                       ? $"\t{deletingPlaylistsPaths.Count - showCount} more...\n" 
                                                       : string.Empty) +
                                                   "Continue?",
                    "('ʘᗩʘ)",
                    MessageBoxButton.OKCancel);
                if (dialogResult != MessageBoxResult.OK)
                {
                    return;
                }
                
                _playlistsExportService.CleanAndExport();

                MessageBox.Show("Export done successfully.", "(ง ͠° ͟ل͜ ͡°)ง");
            }
            catch (Exception e)
            {
                MessageBox.Show($"Unknown error occured.\n" +
                                $"Exception type: {e.GetType()}\n" +
                                $"Message: {e.Message}",
                    "(╥_╥)");
            }
        }

        public void ExportLibraryData()
        {
            using var dialog = new CommonOpenFileDialog()
            {
                IsFolderPicker = true,
                EnsurePathExists = true
            };

            if (dialog.ShowDialog() != CommonFileDialogResult.Ok)
            {
                return;
            }

            try
            {
                _dataExportService.Export(dialog.FileName);

                MessageBox.Show("Экспорт выполнен успешно.", "Ок");
            }
            catch (MusicBeeApiException e)
            {
                MessageBox.Show(e.Message, "Error");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Unknown Error");
            }
        }

        private bool TryGetSingleSelectedFile(out string filePath)
        {
            var queryRes = _mbApi.Library_QueryFilesEx.Invoke("domain=SelectedFiles", out var files);

            if (!queryRes || files.Length != 1)
            {
                filePath = string.Empty;
                return false;
            }

            filePath = files[0];
            return true;
        }
    }
}