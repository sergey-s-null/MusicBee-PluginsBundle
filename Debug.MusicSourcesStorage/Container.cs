using Autofac;
using Debug.Common;
using Module.MusicSourcesStorage;
using Module.MusicSourcesStorage.Core;
using VkNet.Abstractions;

namespace Debug.MusicSourcesStorage;

public static class Container
{
    public static IContainer Create(bool withVkApi)
    {
        var builder = new ContainerBuilder();

        if (withVkApi)
        {
            var vkApi = VkHelper.GetAuthorizedVkApi();
            builder
                .RegisterInstance(vkApi)
                .As<IVkApi>();
        }

        builder.RegisterModule<MusicSourcesStorageModule>();

        builder
            .RegisterType<DebugModuleConfiguration>()
            .As<IModuleConfiguration>()
            .SingleInstance();

        return builder.Build();
    }
}