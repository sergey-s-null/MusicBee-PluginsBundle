using System;
using System.Net;
using System.Windows;
using HackModule.AssemblyBindingRedirect;
using MusicBeePlugin.Factories;
using MusicBeePlugin.GUI.InboxRelocateContextMenu;
using MusicBeePlugin.Services;
using Ninject;
using Ninject.Syntax;
using Root.MusicBeeApi;
using Root.MusicBeeApi.Abstract;
using Root.Services.Abstract;

namespace MusicBeePlugin
{
    public class Plugin
    {
        private const short PluginInfoVersion = 1;
        private const short MinInterfaceVersion = 40;// 41
        private const short MinApiRevision = 53;

        private ISettingsDialogFactory? _settingsDialogFactory;
        private IResourceManager? _resourceManager;
        
        public PluginInfo Initialise(IntPtr apiInterfacePtr)
        {
            AssemblyRedirectService.ApplyRedirects(AppDomain.CurrentDomain);
            
            ServicePointManager.DefaultConnectionLimit = 20;
            
            var mbApiMemoryContainer = new MusicBeeApiMemoryContainer();
            mbApiMemoryContainer.Initialise(apiInterfacePtr);
            
            var kernel = Bootstrapper.GetKernel(mbApiMemoryContainer);

            _settingsDialogFactory = kernel.Get<ISettingsDialogFactory>();
            _resourceManager = kernel.Get<IResourceManager>();
            
            InitSettings();
            CreateMenuItems(kernel);

            return GetPluginInfo();
        }

        private static void CreateMenuItems(IResolutionRoot resolutionRoot)
        {
            var mbApi = resolutionRoot.Get<IMusicBeeApi>();
            var pluginActions = resolutionRoot.Get<IPluginActions>();
            
            mbApi.MB_AddMenuItem(
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
                Name = "Laiser399: Plugins Bundle",
                Description = "Contains list of plugins",
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

        private void InitSettings()
        {
            _resourceManager!.CreateRootIfNeeded();
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
            var result = MessageBox.Show(
                "Delete settings?", 
                "o(╥﹏╥)o", 
                MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                _resourceManager!.DeleteRoot();
            }
        }

        public void ReceiveNotification(string sourceFileUrl, NotificationType type)
        {
            // ignore
        }
    }
}