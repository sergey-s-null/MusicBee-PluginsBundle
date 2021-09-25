using System;
using System.IO;
using System.Net;
using System.Windows;
using HackModule.AssemblyBindingRedirect;
using Microsoft.WindowsAPICodePack.Dialogs;
using Module.ArtworksSearcher.GUI.SearchWindow;
using Module.DataExporter.Exceptions;
using Module.DataExporter.Services;
using Module.InboxAdder.Services;
using Module.PlaylistsExporter.Services;
using Module.VkAudioDownloader.Exceptions;
using Module.VkAudioDownloader.GUI.VkAudioDownloaderWindow;
using Module.VkAudioDownloader.Helpers;
using MusicBeePlugin.GUI.SettingsDialog;
using Ninject;
using Root;

namespace MusicBeePlugin
{
    public class Plugin
    {
        private const short PluginInfoVersion = 1;
        private const short MinInterfaceVersion = 40;// 41
        private const short MinApiRevision = 53;
        
        private IKernel _kernel;
        private MusicBeeApiInterface _mbApi;

        public PluginInfo Initialise(IntPtr apiInterfacePtr)
        {
            AssemblyRedirectService.ApplyRedirects(AppDomain.CurrentDomain);
            
            ServicePointManager.DefaultConnectionLimit = 20;
            
            _mbApi = new MusicBeeApiInterface();
            _mbApi.Initialise(apiInterfacePtr);

            CreateSettingsDirectory();
            
            _kernel = Bootstrapper.GetKernel(_mbApi);

            _mbApi.MB_AddMenuItem("mnuTools/Laiser399: Search Artworks",
                "Laiser399: Search Artworks", (_, __) => SearchArtworks());
            
            _mbApi.MB_AddMenuItem("mnuTools/Laiser399: Download Vk Audio",
                "Laiser399: Download Vk Audio", (_, __) => OpenDownloadDialog());

            _mbApi.MB_AddMenuItem("mnuTools/Laiser399: Add to Library",
                "Laiser399: Add to Library", (_, __) => AddToLibrary());
            
            _mbApi.MB_AddMenuItem("mnuTools/Laiser399: Export Playlists",
                "Laiser399: Export Playlists", (_, __) => ExportPlaylists());
            
            _mbApi.MB_AddMenuItem("mnuTools/Laiser399: Export Library Data",
                "Laiser399: Export Library Data", (_, __) => ExportLibraryData());

            return GetPluginInfo();
        }

        private static PluginInfo GetPluginInfo()
        {
            return new()
            {
                PluginInfoVersion = PluginInfoVersion,
                Name = "Laiser399: VK audios downloader",
                Description = "Download audios from vk and set custom1, custom2 tags",
                Author = "Laiser399",
                TargetApplication = "", //  the name of a Plugin Storage device or panel header for a dockable panel
                Type = PluginType.General,
                VersionMajor = 1, // your plugin version
                VersionMinor = 0,
                Revision = 1,
                MinInterfaceVersion = MinInterfaceVersion,
                MinApiRevision = MinApiRevision,
                ReceiveNotifications = ReceiveNotificationFlags.StartupOnly,
                ConfigurationPanelHeight = 0
            };
        }

        // TODO избавиться или объединить с другими настройками
        private void CreateSettingsDirectory()
        {
            var settingsDirPath = ConfigurationHelper.GetSettingsDirPath(_mbApi);

            try
            {
                if (!Directory.Exists(settingsDirPath))
                {
                    Directory.CreateDirectory(settingsDirPath);
                }
            }
            catch (Exception e)
            {
                throw new InitializeException($"Error creating settings dir: {settingsDirPath}", e);
            }
        }
        
        private void OpenDownloadDialog()
        {
            var wnd = _kernel.Get<VkAudioDownloaderWindow>();
            wnd.ShowDialog();
        }

        private void ExportLibraryData()
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
                var service = _kernel.Get<IDataExportService>();
                service.Export(dialog.FileName);

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

        private void SearchArtworks()
        {
            _mbApi.Library_QueryFilesEx.Invoke("domain=SelectedFiles", out var files);

            if (files is null || files.Length != 1)
            {
                MessageBox.Show("You must select single composition.");
                return;
            }
            
            var filePath = files[0];
            
            var artist = _mbApi.Library_GetFileTag.Invoke(filePath, MetaDataType.Artist);
            var title = _mbApi.Library_GetFileTag.Invoke(filePath, MetaDataType.TrackTitle);
                
            var dialog = _kernel.Get<SearchWindow>();
            if (dialog.ShowDialog(artist, title, out var imageData))
            {
                if (!_mbApi.Library_SetArtworkEx.Invoke(filePath, 0, imageData))
                {
                    MessageBox.Show("Обложка не была сохранена.", "Ошибка");
                }
            }
        }

        private void ExportPlaylists()
        {
            var exportService = _kernel.Get<IPlaylistsExportService>();

            try
            {
                exportService.Export();

                MessageBox.Show("Export done successfully.", "(ง ͠° ͟ل͜ ͡°)ง");
            }
            catch (Exception e)
            {
                // TODO dialog
                Console.WriteLine(e);
                throw;
            }
        }

        private void AddToLibrary()
        {
            _mbApi.Library_QueryFilesEx.Invoke("domain=SelectedFiles", out var files);

            if (files is null || files.Length != 1)
            {
                MessageBox.Show("You must select single item.");
                return;
            }

            _kernel
                .Get<IInboxAddService>()
                .AddToLibrary(files[0]);

            _mbApi.MB_RefreshPanels();
        }
        
        public bool Configure(IntPtr _)
        {
            var dialog = _kernel.Get<SettingsDialog>();
            
            dialog.ShowDialog();
            
            return true;
        }

        public void Uninstall()
        {
            var settingsDirPath = ConfigurationHelper.GetSettingsDirPath(_mbApi);
            
            try
            {
                if (Directory.Exists(settingsDirPath))
                {
                    Directory.Delete(settingsDirPath, true);
                }
            }
            catch (Exception e)
            {
                throw new UninstallException($"Error delete directory: {settingsDirPath}", e);
            }
        }

        public void ReceiveNotification(string sourceFileUrl, NotificationType type)
        {
            // ignore
        }
    }
}