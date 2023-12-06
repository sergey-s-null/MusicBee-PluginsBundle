using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Forms;
using Autofac;
using HackModule.AssemblyBindingRedirect.Services;
using Mead.MusicBee.Api.Services.Abstract;
using Mead.MusicBee.Entities;
using Mead.MusicBee.Enums;
using Mead.MusicBee.Services;
using Module.MusicBee.Extension.Services.Abstract;
using Module.MusicSourcesStorage.Database.Services.Abstract;
using Module.MusicSourcesStorage.Services.Abstract;
using Module.Settings.Database.Services.Abstract;
using Plugin.Main;
using Plugin.Main.Factories;
using Plugin.Main.GUI.Views;
using Plugin.Main.Services;
using MessageBox = System.Windows.MessageBox;

// ReSharper disable once CheckNamespace
namespace MusicBeePlugin;

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

        ApplyMigrations(container);

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
            Name = "Plugins Bundle",
            Description = "Contains list of plugins",
            Author = "s.s.d",
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

    private static void ApplyMigrations(IContainer container)
    {
        container
            .Resolve<ISettingsDbMigrator>()
            .UpdateToLatest();

        container
            .Resolve<IMusicSourcesStorageMigrator>()
            .UpdateToLatest();
    }

    private static void ApplyAssembliesResolution()
    {
        var assembliesDirectory = Path.Combine(Environment.CurrentDirectory, "Plugins");
        var assemblyResolver = new AssemblyResolver(assembliesDirectory);
        AppDomain.CurrentDomain.AssemblyResolve += assemblyResolver.ResolveHandler;
    }

    private static void CreateMenuItems(IComponentContext container)
    {
        var mbApi = container.Resolve<IMusicBeeApi>();
        var pluginActions = container.Resolve<IPluginActions>();
        var musicSourcesStorageModuleActions = container.Resolve<IMusicSourcesStorageModuleActions>();
        var inboxRelocateContextMenuFactory = container.Resolve<Func<InboxRelocateContextMenu>>();

        mbApi.MB_AddMenuItem(
            "mnuTools/s.s.d: Search Artworks",
            "s.s.d: Search Artworks",
            (_, _) => pluginActions.SearchArtworks()
        );

        mbApi.MB_AddMenuItem(
            "mnuTools/s.s.d: Download Vk Audio",
            "s.s.d: Download Vk Audio",
            (_, _) => pluginActions.DownloadVkAudios()
        );

        mbApi.MB_AddMenuItem(
            "mnuTools/s.s.d: Compare Vk And Local Audios",
            "s.s.d: Compare Vk And Local Audios",
            (_, _) => pluginActions.CompareVkAndLocalAudios()
        );

        mbApi.MB_AddMenuItem(
            "mnuTools/s.s.d: Add to Library",
            "s.s.d: Add to Library",
            (_, _) => pluginActions.AddSelectedFileToLibrary()
        );

        mbApi.MB_AddMenuItem(
            "mnuTools/s.s.d: Export Playlists",
            "s.s.d: Export Playlists",
            (_, _) => pluginActions.ExportPlaylists()
        );

        mbApi.MB_AddMenuItem(
            "mnuTools/s.s.d: Export Library Data",
            "s.s.d: Export Library Data",
            (_, _) => pluginActions.ExportLibraryData()
        );

        mbApi.MB_AddMenuItem(
            "mnuTools/s.s.d: Inbox relocate context menu",
            "s.s.d: Open inbox relocate context menu",
            (_, _) =>
            {
                var inboxRelocateContextMenu = inboxRelocateContextMenuFactory();
                inboxRelocateContextMenu.IsOpen = true;
            }
        );

        var musicSourcesStorageRootItem = (ToolStripMenuItem)mbApi.MB_AddMenuItem(
            "mnuTools/s.s.d: Music sources storage",
            "s.s.d: Music sources storage plugin actions",
            (_, _) => { }
        );
        musicSourcesStorageRootItem.AllowDrop = true;
        musicSourcesStorageRootItem.DropDownItems.Add(mbApi.MB_AddMenuItem(
            "Show music sources",
            "Open dialog with music sources",
            (_, _) => musicSourcesStorageModuleActions.ShowMusicSources()
        ));
        musicSourcesStorageRootItem.DropDownItems.Add(mbApi.MB_AddMenuItem(
            "Add Vk post with archive source",
            "Open wizard for adding new source",
            (_, _) => musicSourcesStorageModuleActions.AddVkPostWithArchiveSource()
        ));
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