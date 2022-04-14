using Microsoft.Extensions.DependencyInjection;
using Module.VkAudioDownloader.GUI.AbstractViewModels;
using Module.VkAudioDownloader.GUI.Factories;
using Module.VkAudioDownloader.GUI.ViewModels;
using Module.VkAudioDownloader.Settings;
using Ninject.Extensions.Factory;
using Ninject.Modules;
using VkNet;
using VkNet.Abstractions;
using VkNet.AudioBypassService.Extensions;

namespace Module.VkAudioDownloader
{
    public class MusicDownloaderModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IVkApi>().ToConstant(GetVkApi());
            Bind<IMusicDownloaderSettings>()
                .To<MusicDownloaderSettings>()
                .InSingletonScope();

            // ViewModels
            Bind<IVkAudioDownloaderWindowVM>()
                .To<VkAudioDownloaderWindowVM>();
            Bind<IMusicDownloaderSettingsVM>()
                .To<MusicDownloaderSettingsVM>();
            Bind<IAuthorizationWindowVM>()
                .To<AuthorizationWindowVM>();

            // Factories
            Bind<IVkAudioDownloaderWindowFactory>()
                .ToFactory();
            Bind<IAuthorizationWindowFactory>()
                .ToFactory();
        }

        private static IVkApi GetVkApi()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddAudioBypass();
            return new VkApi(serviceCollection);
        }
    }
}