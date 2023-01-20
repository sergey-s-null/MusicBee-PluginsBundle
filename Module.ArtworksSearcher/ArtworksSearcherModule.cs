using Autofac;
using Module.ArtworksSearcher.GUI.SearchWindow;
using Module.ArtworksSearcher.GUI.Settings;
using Module.ArtworksSearcher.ImagesProviders;
using Module.ArtworksSearcher.Services;
using Module.ArtworksSearcher.Services.Abstract;
using Module.ArtworksSearcher.Settings;
using ArtworksSearcherSettings = Module.ArtworksSearcher.Settings.ArtworksSearcherSettings;

namespace Module.ArtworksSearcher
{
    public sealed class ArtworksSearcherModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<ArtworksSearcherSettings>()
                .As<IArtworksSearcherSettings>()
                .SingleInstance();

            builder
                .RegisterType<ArtworksSearcherSettingsVM>()
                .As<IArtworksSearcherSettingsVM>();

            builder
                .RegisterType<ImagesProvidersFactory>()
                .As<IImagesProvidersFactory>()
                .SingleInstance();

            builder
                .RegisterType<OsuImagesProvider>()
                .AsSelf();
            builder
                .RegisterType<GoogleImagesProvider>()
                .AsSelf();
            builder
                .RegisterType<SearchWindowVM>()
                .AsSelf();
            builder
                .RegisterType<SearchWindow>()
                .AsSelf();

            builder
                .RegisterType<GoogleImageSearchService>()
                .As<IGoogleImageSearchService>()
                .SingleInstance();
        }
    }
}