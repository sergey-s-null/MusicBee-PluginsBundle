using Microsoft.Extensions.DependencyInjection;
using Ninject.Modules;
using Root;
using VkMusicDownloader.GUI.Settings;
using VkMusicDownloader.Helpers;
using VkMusicDownloader.Settings;
using VkNet;
using VkNet.AudioBypassService.Extensions;

namespace VkMusicDownloader
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
            Bind<VkApi>().ToConstant(GetVkApi());
            Bind<IMusicDownloaderSettings>()
                .To<MusicDownloaderSettings>()
                .WithConstructorArgument("filePath",
                    ConfigurationHelper.GetSettingsFilePath(_mbApi));
            Bind<IMusicDownloaderSettingsVM>()
                .To<MusicDownloaderSettingsVM>();
        }
        
        private static VkApi GetVkApi()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddAudioBypass();
            return new VkApi(serviceCollection);
        }
    }
}