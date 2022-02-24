using Module.ArtworksSearcher;
using Module.DataExporter;
using Module.InboxAdder;
using Module.PlaylistsExporter;
using Module.VkAudioDownloader;
using MusicBeePlugin.Factories;
using MusicBeePlugin.GUI.SettingsDialog;
using MusicBeePlugin.Services;
using Ninject;
using Ninject.Extensions.Factory;
using Root.MusicBeeApi;

namespace MusicBeePlugin
{
    public static class Bootstrapper
    {
        public static IKernel GetKernel(MusicBeeApiMemoryContainer mbApi)
        {
            var kernel = new StandardKernel();

            kernel.Bind<MusicBeeApiMemoryContainer>()
                .ToConstant(mbApi);
            
            kernel.Load(new MusicDownloaderModule(mbApi));
            kernel.Load(new ArtworksSearcherModule(mbApi));
            kernel.Load(new PlaylistsExporterModule(mbApi));
            kernel.Load(new InboxAdderModule());
            kernel.Load(new DataExporterModule());

            kernel
                .Bind<IPluginActions>()
                .To<PluginActions>()
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