using Autofac;
using Module.MusicSourcesStorage.Gui;

namespace Module.MusicSourcesStorage;

public sealed class MusicSourcesStorageModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterModule<DIModule>();
        builder.RegisterModule<Logic.DIModule>();
        builder.RegisterModule<Database.DIModule>();
    }
}