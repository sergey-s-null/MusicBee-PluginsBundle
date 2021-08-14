using Module.ArtworksSearcher;
using Module.VkMusicDownloader;
using Ninject;
using Root;

namespace MusicBeePlugin
{
    public static class Bootstrapper
    {
        public static IKernel GetKernel(MusicBeeApiInterface mbApi)
        {
            var kernel = new StandardKernel(
                new MusicDownloaderModule(mbApi),
                new ArtworksSearcherModule(mbApi));




            return kernel;
        }
    }
}