using Autofac;
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
    }
}