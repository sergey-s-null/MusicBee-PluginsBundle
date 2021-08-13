using System;
using System.IO;
using System.Net;
using System.Windows;
using Microsoft.WindowsAPICodePack.Dialogs;
using Module.DataExporter;
using Module.DataExporter.Exceptions;
using Module.VkMusicDownloader.Exceptions;
using Module.VkMusicDownloader.GUI.MusicDownloaderWindow;
using Module.VkMusicDownloader.Helpers;
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
            ServicePointManager.DefaultConnectionLimit = 20;
            
            _mbApi = new MusicBeeApiInterface();
            _mbApi.Initialise(apiInterfacePtr);

            CreateSettingsDirectory();
            
            _kernel = Bootstrapper.GetKernel(_mbApi);
            
            _mbApi.MB_AddMenuItem("mnuTools/Laiser399: download vk audio",
                "Laiser399: download vk audio", (_, __) => OpenDownloadDialog());

            _mbApi.MB_AddMenuItem("mnuTools/Laiser399: Export",
                "Laiser399: Export", (_, __) => Export());

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
            var wnd = _kernel.Get<MusicDownloaderWindow>();
            wnd.ShowDialog();
        }

        private void Export()
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
                var service = _kernel.Get<DataExportService>();
                service.Service(dialog.FileName);

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