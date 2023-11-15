using Autofac;
using Module.MusicSourcesStorage;

namespace Debug.MusicSourcesStorage;

public static class Container
{
    public static IContainer Create()
    {
        var builder = new ContainerBuilder();

        builder.RegisterModule<MusicSourcesStorageModule>();

        return builder.Build();
    }
}