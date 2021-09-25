using Module.ArtworksSearcher;
using Module.DataExporter;
using Module.InboxAdder;
using Module.PlaylistsExporter;
using Module.VkAudioDownloader;
using MusicBeePlugin.GUI.SettingsDialog;
using MusicBeePlugin.Services;
using Ninject;
using Root;

namespace MusicBeePlugin
{
    public static class Bootstrapper
    {
        public static IKernel GetKernel(MusicBeeApiInterface mbApi)
        {
            var kernel = new StandardKernel();

            kernel.Bind<MusicBeeApiInterface>()
                .ToConstant(mbApi);
            
            kernel.Load(new MusicDownloaderModule(mbApi));
            kernel.Load(new ArtworksSearcherModule(mbApi));
            kernel.Load(new PlaylistsExporterModule(mbApi));
            kernel.Load(new InboxAdderModule());
            kernel.Load(new DataExporterModule());

            kernel
                .Bind<IMusicBeeInboxAddService>()
                .To<MusicBeeInboxAddService>()
                .InSingletonScope();
            
            kernel.Bind<SettingsDialogVM>().ToSelf();
            
            return kernel;
        }
    }
}