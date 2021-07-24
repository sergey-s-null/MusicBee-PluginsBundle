using MusicBeePlugin;
using Ninject;

namespace VkMusicDownloader.Ex
{
    public static class KernelEx
    {
        public static Plugin.MusicBeeApiInterface GetMbApi(this IReadOnlyKernel kernel)
        {
            return kernel.Get<Plugin.MusicBeeApiInterface>();
        }
    }
}