using Module.ArtworksSearcher;
using Module.PlaylistsExporter;
using Module.VkMusicDownloader;
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
            
            return kernel;
        }
    }
}