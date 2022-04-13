using Microsoft.Extensions.DependencyInjection;
using Module.VkAudioDownloader.Factories;
using Module.VkAudioDownloader.GUI.AbstractViewModels;
using Module.VkAudioDownloader.GUI.ViewModels;
using Module.VkAudioDownloader.Settings;
using Ninject.Extensions.Factory;
using Ninject.Modules;
using VkNet;
using VkNet.Abstractions;
using VkNet.AudioBypassService.Extensions;
using MusicDownloaderSettings = Module.VkAudioDownloader.Settings.MusicDownloaderSettings;

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
            Bind<IMusicDownloaderSettingsVM>()
                .To<MusicDownloaderSettingsVM>();
            Bind<IVkAudioDownloaderWindowFactory>()
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