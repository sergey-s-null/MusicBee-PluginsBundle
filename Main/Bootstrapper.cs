using Ninject;
using Root;
using VkMusicDownloader;

namespace MusicBeePlugin
{
    public static class Bootstrapper
    {
        public static IKernel GetKernel(MusicBeeApiInterface mbApi)
        {
            var kernel = new StandardKernel(new MusicDownloaderModule(mbApi));




            return kernel;
        }
    }
}