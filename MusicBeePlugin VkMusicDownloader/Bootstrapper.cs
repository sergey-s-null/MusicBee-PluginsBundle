using Microsoft.Extensions.DependencyInjection;
using MusicBeePlugin;
using Ninject;
using VkNet;
using VkNet.AudioBypassService.Extensions;

namespace VkMusicDownloader
{
    public class Bootstrapper
    {
        public static IReadOnlyKernel GetKernel(Plugin.MusicBeeApiInterface mbApiInterface)
        {
            var kernel = new KernelConfiguration();
            
            kernel.Bind<Plugin.MusicBeeApiInterface>().ToConstant(mbApiInterface);
            
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddAudioBypass();
            var vkApi = new VkApi(serviceCollection);
            kernel.Bind<VkApi>().ToConstant(vkApi);
            
            
            return kernel.BuildReadonlyKernel();
        }
    }
}