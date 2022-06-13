using Module.ArtworksSearcher.Factories;
using Module.ArtworksSearcher.GUI.SearchWindow;
using Module.ArtworksSearcher.GUI.Settings;
using Module.ArtworksSearcher.ImagesProviders;
using Module.ArtworksSearcher.Services;
using Module.ArtworksSearcher.Services.Abstract;
using Module.ArtworksSearcher.Settings;
using Ninject.Extensions.Factory;
using Ninject.Modules;
using ArtworksSearcherSettings = Module.ArtworksSearcher.Settings.ArtworksSearcherSettings;

namespace Module.ArtworksSearcher
{
    public class ArtworksSearcherModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IArtworksSearcherSettings>()
                .To<ArtworksSearcherSettings>()
                .InSingletonScope();

            Bind<IArtworksSearcherSettingsVM>()
                .To<ArtworksSearcherSettingsVM>();

            Bind<IGoogleImagesEnumeratorFactory>()
                .ToFactory()
                .InSingletonScope();
            Bind<IImagesProvidersFactory>()
                .To<ImagesProvidersFactory>()
                .InSingletonScope();

            Bind<ISearchWindowFactory>()
                .ToFactory();

            Bind<OsuImagesProvider>().ToSelf();
            Bind<GoogleImagesProvider>().ToSelf();
            Bind<SearchWindowVM>().ToSelf();
            Bind<SearchWindow>().ToSelf();

            Bind<IGoogleImageSearchService>()
                .To<GoogleImageSearchService>()
                .InSingletonScope();
        }
    }
}