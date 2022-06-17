using HackModule.AssemblyBindingRedirect;
using Module.ArtworksSearcher;
using Module.AudioSourcesComparer;
using Module.DataExporter;
using Module.InboxAdder;
using Module.PlaylistsExporter;
using Module.Vk;
using Module.VkAudioDownloader;
using MusicBeePlugin.Factories;
using MusicBeePlugin.GUI.ViewModels;
using MusicBeePlugin.Services;
using Ninject;
using Ninject.Extensions.Factory;
using Root;
using Root.MusicBeeApi;
using Root.MusicBeeApi.Abstract;
using Root.Services.Abstract;

namespace MusicBeePlugin
{
    public static class Bootstrapper
    {
        public static IKernel GetKernel(MusicBeeApiMemoryContainer mbApiMemoryContainer)
        {
            var kernel = new StandardKernel();

            kernel
                .Bind<MusicBeeApiMemoryContainer>()
                .ToConstant(mbApiMemoryContainer);
            kernel
                .Bind<IMusicBeeApi>()
                .To<MusicBeeApiMemoryContainerWrapper>()
                .InSingletonScope();

            kernel.Load<RootModule>();
            kernel.Load(new VkModule(true));
            kernel.Load<MusicDownloaderModule>();
            kernel.Load<ArtworksSearcherModule>();
            kernel.Load<PlaylistsExporterModule>();
            kernel.Load<InboxAdderModule>();
            kernel.Load<DataExporterModule>();
            kernel.Load<AudioSourcesComparerModule>();

            kernel
                .Bind<IPluginActions>()
                .To<PluginActions>()
                .InSingletonScope();

            kernel
                .Bind<IVkApiAuthorizationsService>()
                .To<VkApiAuthorizationsService>()
                .InSingletonScope();

            kernel
                .Bind<ISettingsDialogFactory>()
                .ToFactory();

            kernel.Bind<SettingsDialogVM>().ToSelf();

            if (!kernel.HasModule("Ninject.Extensions.Factory.FuncModule"))
            {
                kernel.Load(new FuncModule());
            }

            return kernel;
        }
    }
}