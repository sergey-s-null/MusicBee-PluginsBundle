using Autofac;
using Module.MusicSourcesStorage.Database.Factories;
using Module.MusicSourcesStorage.Database.Factories.Abstract;
using Module.MusicSourcesStorage.Database.Services;
using Module.MusicSourcesStorage.Database.Services.Abstract;

namespace Module.MusicSourcesStorage.Database;

public sealed class DIModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterType<MusicSourcesRepository>()
            .As<IMusicSourcesRepository>()
            .SingleInstance();
        builder
            .RegisterType<MusicSourcesStorageMigrator>()
            .As<IMusicSourcesStorageMigrator>()
            .SingleInstance();
        builder
            .RegisterType<MusicSourcesStorageContextFactory>()
            .As<IMusicSourcesStorageContextFactory>()
            .SingleInstance();
    }
}