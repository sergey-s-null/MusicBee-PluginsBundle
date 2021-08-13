using Microsoft.Extensions.DependencyInjection;
using Module.VkMusicDownloader.GUI.Settings;
using Module.VkMusicDownloader.Helpers;
using Module.VkMusicDownloader.Settings;
using Ninject.Modules;
using Root;
using VkNet;
using VkNet.Abstractions;
using VkNet.AudioBypassService.Extensions;

namespace Module.VkMusicDownloader
{
    public class MusicDownloaderModule : NinjectModule
    {
        private readonly MusicBeeApiInterface _mbApi;
        
        public MusicDownloaderModule(MusicBeeApiInterface mbApi)
        {
            _mbApi = mbApi;
        }
        
        public override void Load()
        {
            Bind<MusicBeeApiInterface>().ToConstant(_mbApi);
            Bind<IVkApi>().ToConstant(GetVkApi());
            Bind<IMusicDownloaderSettings>()
                .To<MusicDownloaderSettings>()
                .WithConstructorArgument("filePath",
                    ConfigurationHelper.GetSettingsFilePath(_mbApi));
            Bind<IMusicDownloaderSettingsVM>()
                .To<MusicDownloaderSettingsVM>();
        }
        
        private static IVkApi GetVkApi()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddAudioBypass();
            return new VkApi(serviceCollection);
        }
    }
}