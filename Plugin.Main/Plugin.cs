using System;
using System.IO;
using System.Net;
using System.Windows;
using Autofac;
using HackModule.AssemblyBindingRedirect.Services;
using Mead.MusicBee.Entities;
using Mead.MusicBee.Enums;
using Mead.MusicBee.Services;
using Module.MusicBee.Autogen.Services.Abstract;
using Module.MusicBee.Extension.Services.Abstract;
using MusicBeePlugin.Factories;
using MusicBeePlugin.GUI.Views;
using MusicBeePlugin.Services;

namespace MusicBeePlugin
{
    public class Plugin : PluginBase
    {
        private const short PluginInfoVersion = 1;
        private const short MinInterfaceVersion = 40; // 41
        private const short MinApiRevision = 53;

        private SettingsDialogFactory? _settingsDialogFactory;
        private IResourceManager? _resourceManager;

        static Plugin()
        {
            ApplyAssembliesResolution();
            ServicePointManager.DefaultConnectionLimit = 20;
        }

        protected override void OnMusicBeeApiProvided(MusicBeeApiMemoryContainer musicBeeApi)
        {
            var container = PluginContainer.Create(musicBeeApi);

            _settingsDialogFactory = container.Resolve<SettingsDialogFactory>();
            _resourceManager = container.Resolve<IResourceManager>();

            InitSettings();
            CreateMenuItems(container);
        }

        protected override PluginInfo GetPluginInfo()
        {
            return new PluginInfo
            {
                PluginInfoVersion = PluginInfoVersion,
                Name = "Laiser399: Plugins Bundle",
                Description = "Contains list of plugins",
                Author = "Laiser399",
                TargetApplication = "",
                Type = PluginType.General,
                VersionMajor = 1,
                VersionMinor = 0,
                Revision = 1,
                MinInterfaceVersion = MinInterfaceVersion,
                MinApiRevision = MinApiRevision,
                ReceiveNotifications = ReceiveNotificationFlags.StartupOnly,
                ConfigurationPanelHeight = 0
            };
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

        private void InitSettings()
        {
            _resourceManager!.CreateRootIfNeeded();
        }

        public override bool Configure(IntPtr _)
        {
            _settingsDialogFactory?
                .Invoke()
                .ShowDialog();

            return true;
        }

        public override void Uninstall()
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
    }
}