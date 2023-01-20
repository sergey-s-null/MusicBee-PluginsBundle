using System;
using System.IO;
using System.Net;
using System.Windows;
using Autofac;
using HackModule.AssemblyBindingRedirect.Services;
using HackModule.AssemblyBindingRedirect.Services.Abstract;
using MusicBeePlugin.Factories;
using MusicBeePlugin.GUI.Views;
using MusicBeePlugin.Services;
using Root.MusicBeeApi;
using Root.MusicBeeApi.Abstract;
using Root.Services.Abstract;

namespace MusicBeePlugin
{
    public class Plugin
    {
        private const short PluginInfoVersion = 1;
        private const short MinInterfaceVersion = 40; // 41
        private const short MinApiRevision = 53;

        private SettingsDialogFactory? _settingsDialogFactory;
        private IResourceManager? _resourceManager;

        static Plugin()
        {
            ApplyAssembliesResolution();
        }
        
        public PluginInfo Initialise(IntPtr apiInterfacePtr)
        {
            ServicePointManager.DefaultConnectionLimit = 20;

            var mbApiMemoryContainer = new MusicBeeApiMemoryContainer();
            mbApiMemoryContainer.Initialise(apiInterfacePtr);

            var container = PluginContainer.Create(mbApiMemoryContainer);

            _settingsDialogFactory = container.Resolve<SettingsDialogFactory>();
            _resourceManager = container.Resolve<IResourceManager>();

            InitSettings();
            CreateMenuItems(container);

            return GetPluginInfo();
        }

        private static void ApplyAssembliesResolution()
        {
            var assembliesDirectory = Path.Combine(Environment.CurrentDirectory, "Plugins");
            var assemblyResolver = new AssemblyResolver(assembliesDirectory);
            AppDomain.CurrentDomain.AssemblyResolve += assemblyResolver.ResolveHandler;
        }

        private static void CreateMenuItems(IContainer container)
        {
            var mbApi = container.Resolve<IMusicBeeApi>();
            var pluginActions = container.Resolve<IPluginActions>();

            mbApi.MB_AddMenuItem(
                "mnuTools/Laiser399: Search Artworks",
                "Laiser399: Search Artworks",
                (_, _) => pluginActions.SearchArtworks());

            mbApi.MB_AddMenuItem(
                "mnuTools/Laiser399: Download Vk Audio",
                "Laiser399: Download Vk Audio",
                (_, _) => pluginActions.DownloadVkAudios());

            mbApi.MB_AddMenuItem(
                "mnuTools/Laiser399: Compare Vk And Local Audios",
                "Laiser399: Compare Vk And Local Audios",
                (_, _) => pluginActions.CompareVkAndLocalAudios());

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
                    var inboxRelocateContextMenu = container.LoadInboxRelocateContextMenu();
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
                .Invoke()
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