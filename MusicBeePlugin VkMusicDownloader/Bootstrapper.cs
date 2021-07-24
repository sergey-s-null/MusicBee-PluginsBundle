using Microsoft.Extensions.DependencyInjection;
using MusicBeePlugin;
using Ninject;
using VkMusicDownloader.Helpers;
using VkMusicDownloader.Settings;
using VkNet;
using VkNet.AudioBypassService.Extensions;

namespace VkMusicDownloader
{
    public static class Bootstrapper
    {
        public static IReadOnlyKernel GetKernel(Plugin.MusicBeeApiInterface mbApi)
        {
            var kernel = new KernelConfiguration();
            
            kernel.Bind<Plugin.MusicBeeApiInterface>().ToConstant(mbApi);
            kernel.Bind<VkApi>().ToConstant(GetVkApi());
            kernel.Bind<IMusicDownloaderSettings>()
                .To<MusicDownloaderSettings>()
                .WithConstructorArgument("filePath",
                    ConfigurationHelper.GetSettingsFilePath(mbApi));

            return kernel.BuildReadonlyKernel();
        }

        private static VkApi GetVkApi()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddAudioBypass();
            return new VkApi(serviceCollection);
        }
    }
}