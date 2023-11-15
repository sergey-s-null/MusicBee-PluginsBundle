using Autofac;
using Module.MusicSourcesStorage;
using Module.MusicSourcesStorage.Core.Entities.Abstract;

namespace Debug.MusicSourcesStorage;

public static class Container
{
    public static IContainer Create()
    {
        var builder = new ContainerBuilder();

        builder.RegisterModule<MusicSourcesStorageModule>();

        builder
            .RegisterType<DebugModuleConfiguration>()
            .As<IModuleConfiguration>()
            .SingleInstance();

        return builder.Build();
    }
}