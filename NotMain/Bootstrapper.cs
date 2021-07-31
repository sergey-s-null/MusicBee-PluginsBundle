using Ninject;
using Root;
using VkMusicDownloader;

namespace MusicBeePlugin
{
    public static class Bootstrapper
    {
        public static IReadOnlyKernel GetKernel(MusicBeeApiInterface mbApi)
        {
            var kernel = new KernelConfiguration(new MusicDownloaderModule(mbApi));

            
            

            return kernel.BuildReadonlyKernel();
        }
    }
}