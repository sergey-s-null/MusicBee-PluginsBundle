using Module.ArtworksSearcher.Factories;
using Module.ArtworksSearcher.GUI.Settings;
using Module.ArtworksSearcher.Helpers;
using Module.ArtworksSearcher.Settings;
using Ninject.Modules;
using Root;

namespace Module.ArtworksSearcher
{
    public class ArtworksSearcherModule : NinjectModule
    {
        private readonly MusicBeeApiInterface _mbApi;
        
        public ArtworksSearcherModule(MusicBeeApiInterface mbApi)
        {
            _mbApi = mbApi;
        }
        
        public override void Load()
        {
            Bind<IArtworksSearcherSettings>()
                .To<ArtworksSearcherSettings>()
                .InSingletonScope()
                .WithConstructorArgument("filePath",
                    ConfigurationHelper.GetSettingsFilePath(_mbApi));
            
            Bind<IArtworksSearcherSettingsVM>()
                .To<ArtworksSearcherSettingsVM>();

            Bind<IGoogleImagesEnumeratorFactory>()
                .To<GoogleImagesEnumeratorFactory>()
                .InSingletonScope();
        }
    }
}