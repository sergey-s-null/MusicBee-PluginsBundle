using Module.VkAudioDownloader.GUI.AbstractViewModels;
using Module.VkAudioDownloader.GUI.Factories;
using Module.VkAudioDownloader.GUI.ViewModels;
using Module.VkAudioDownloader.Settings;
using Ninject.Extensions.Factory;
using Ninject.Modules;

namespace Module.VkAudioDownloader
{
    public sealed class MusicDownloaderModule : NinjectModule
    {
        public override void Load()
        {
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
    }
}