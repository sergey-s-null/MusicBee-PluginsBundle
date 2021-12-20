using System;
using System.IO;
using System.Net;
using HackModule.AssemblyBindingRedirect;
using Module.VkAudioDownloader.Exceptions;
using Module.VkAudioDownloader.Helpers;
using MusicBeePlugin.Factories;
using MusicBeePlugin.GUI.InboxRelocateContextMenu;
using MusicBeePlugin.Services;
using Ninject;
using Ninject.Syntax;
using Root;

namespace MusicBeePlugin
{
    public class Plugin
    {
        private const short PluginInfoVersion = 1;
        private const short MinInterfaceVersion = 40;// 41
        private const short MinApiRevision = 53;

        private ISettingsDialogFactory? _settingsDialogFactory;
        private string? _settingsDirPath;
        
        public PluginInfo Initialise(IntPtr apiInterfacePtr)
        {
            AssemblyRedirectService.ApplyRedirects(AppDomain.CurrentDomain);
            
            ServicePointManager.DefaultConnectionLimit = 20;
            
            var mbApi = new MusicBeeApiInterface();
            mbApi.Initialise(apiInterfacePtr);

            CreateSettingsDirectory(mbApi);
            
            var kernel = Bootstrapper.GetKernel(mbApi);

            _settingsDialogFactory = kernel.Get<ISettingsDialogFactory>();
            _settingsDirPath = ConfigurationHelper.GetSettingsDirPath(mbApi);
            
            CreateMenuItems(kernel);

            return GetPluginInfo();
        }

        private static void CreateMenuItems(IResolutionRoot resolutionRoot)
        {
            var mbApi = resolutionRoot.Get<MusicBeeApiInterface>();
            var pluginActions = resolutionRoot.Get<IPluginActions>();
            
            mbApi.MB_AddMenuItem!(
                "mnuTools/Laiser399: Search Artworks",
                "Laiser399: Search Artworks", 
                (_, _) => pluginActions.SearchArtworks());
            
            mbApi.MB_AddMenuItem(
                "mnuTools/Laiser399: Download Vk Audio",
                "Laiser399: Download Vk Audio", 
                (_, _) => pluginActions.DownloadVkAudios());

            mbApi.MB_AddMenuItem(
                "mnuTools/Laiser399: Add to Library",
                "Laiser399: Add to Library", 
                (_, _) => pluginActions.AddSelectedFileToLibrary());
            
            mbApi.MB_AddMenuItem(
                "mnuTools/Laiser399: Export Playlists",
                "Laiser399: Export Playlists", 
                (_, _) => pluginActions.ExportPlaylists());
            
            mbApi.MB_AddMenuItem(
                "mnuTools/Laiser399: Export Library Data",
                "Laiser399: Export Library Data", 
                (_, _) => pluginActions.ExportLibraryData());

            mbApi.MB_AddMenuItem(
                "mnuTools/Laiser399: Inbox relocate context menu",
                "Laiser399: Inbox relocate context menu",
                (_, _) =>
                {
                    var inboxRelocateContextMenu = resolutionRoot.LoadInboxRelocateContextMenu();
                    inboxRelocateContextMenu.IsOpen = true;
                });
        }
        
        private static PluginInfo GetPluginInfo()
        {
            return new PluginInfo
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
        private static void CreateSettingsDirectory(MusicBeeApiInterface mbApi)
        {
            var settingsDirPath = ConfigurationHelper.GetSettingsDirPath(mbApi);

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
        
        public bool Configure(IntPtr _)
        {
            _settingsDialogFactory?
                .Create()
                .ShowDialog();
            
            return true;
        }

        public void Uninstall()
        {
            try
            {
                if (Directory.Exists(_settingsDirPath))
                {
                    Directory.Delete(_settingsDirPath!, true);
                }
            }
            catch (Exception e)
            {
                throw new UninstallException($"Error delete directory: {_settingsDirPath}", e);
            }
        }

        public void ReceiveNotification(string sourceFileUrl, NotificationType type)
        {
            // ignore
        }
    }
}