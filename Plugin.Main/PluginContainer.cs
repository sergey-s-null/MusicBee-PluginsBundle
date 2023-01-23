using Autofac;
using Module.ArtworksSearcher;
using Module.AudioSourcesComparer;
using Module.DataExporter;
using Module.InboxAdder;
using Module.MusicBee;
using Module.MusicBee.Autogen;
using Module.MusicBee.Extension;
using Module.MusicBee.Services;
using Module.PlaylistsExporter;
using Module.Vk;
using Module.VkAudioDownloader;
using MusicBeePlugin.GUI.ViewModels;
using MusicBeePlugin.GUI.Views;
using MusicBeePlugin.Services;
using Newtonsoft.Json.Linq;
using Root.Services.Abstract;

namespace MusicBeePlugin
{
    public static class PluginContainer
    {
        public static IContainer Create(MusicBeeApiMemoryContainer mbApiMemoryContainer)
        {
            var builder = new ContainerBuilder();

            builder
                .Register(_ => mbApiMemoryContainer)
                .AsSelf();

            builder.RegisterModule<MusicBeeAutogenModule>();
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
                .RegisterType<SettingsDialog>()
                .AsSelf();

            return builder.Build();
        }
    }
}