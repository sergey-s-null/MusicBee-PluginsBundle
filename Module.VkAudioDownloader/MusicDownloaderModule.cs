using Microsoft.Extensions.DependencyInjection;
using Module.VkAudioDownloader.Factories;
using Module.VkAudioDownloader.GUI.Settings;
using Module.VkAudioDownloader.Helpers;
using Module.VkAudioDownloader.Settings;
using Ninject.Extensions.Factory;
using Ninject.Modules;
using Root.MusicBeeApi.Abstract;
using VkNet;
using VkNet.Abstractions;
using VkNet.AudioBypassService.Extensions;
using MusicDownloaderSettings = Module.VkAudioDownloader.Settings.MusicDownloaderSettings;

namespace Module.VkAudioDownloader
{
    public class MusicDownloaderModule : NinjectModule
    {
        private readonly IMusicBeeApi _mbApi;
        
        public MusicDownloaderModule(IMusicBeeApi mbApi)
        {
            _mbApi = mbApi;
        }
        
        public override void Load()
        {
            Bind<IVkApi>().ToConstant(GetVkApi());
            Bind<IMusicDownloaderSettings>()
                .To<MusicDownloaderSettings>()
                .InSingletonScope()
                .WithConstructorArgument("filePath",
                    ConfigurationHelper.GetSettingsFilePath(_mbApi));
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