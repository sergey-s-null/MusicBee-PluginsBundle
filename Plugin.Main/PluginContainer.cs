using Autofac;
using Mead.MusicBee.Api.Autofac.DependencyInjection.Extensions;
using Mead.MusicBee.Services;
using Module.ArtworksSearcher;
using Module.AudioSourcesComparer;
using Module.Core.Services.Abstract;
using Module.DataExporter;
using Module.InboxAdder;
using Module.MusicBee.Extension;
using Module.PlaylistsExporter;
using Module.Vk;
using Module.VkAudioDownloader;
using Plugin.Main.GUI.ViewModels;
using Plugin.Main.GUI.Views;
using Plugin.Main.Services;

namespace Plugin.Main;

public static class PluginContainer
{
    public static IContainer Create(MusicBeeApiMemoryContainer mbApiMemoryContainer)
    {
        var builder = new ContainerBuilder();

        builder.RegisterMusicBeeApi(mbApiMemoryContainer);

        builder.RegisterModule<MusicBeeExtensionModule>();
        builder.RegisterModule(new VkModule(true));
        builder.RegisterModule<MusicDownloaderModule>();
        builder.RegisterModule<ArtworksSearcherModule>();
        builder.RegisterModule<PlaylistsExporterModule>();
        builder.RegisterModule<InboxAdderModule>();
        builder.RegisterModule<DataExporterModule>();
        builder.RegisterModule<AudioSourcesComparerModule>();

        builder
            .RegisterType<PluginActions>()
            .As<IPluginActions>()
            .SingleInstance();

        builder
            .RegisterType<VkApiAuthorizationsService>()
            .As<IVkApiAuthorizationsService>()
            .SingleInstance();

        builder
            .RegisterType<SettingsDialogVM>()
            .AsSelf();
        builder
            .RegisterType<InboxRelocateContextMenuVM>()
            .AsSelf();
        builder
            .RegisterType<InboxRelocateContextMenuVM>()
            .AsSelf();

        builder
            .RegisterType<SettingsDialog>()
            .AsSelf();
        builder
            .RegisterType<InboxRelocateContextMenu>()
            .AsSelf();

        return builder.Build();
    }
}