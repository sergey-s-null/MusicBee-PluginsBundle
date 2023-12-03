using Autofac;
using Debug.Common;
using Module.MusicSourcesStorage;
using Module.MusicSourcesStorage.Core;
using Module.Vk.Gui.Services.Abstract;
using Moq;
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

            builder
                .RegisterInstance(GetVkApiProviderMock(vkApi))
                .As<IVkApiProvider>()
                .SingleInstance();
        }

        builder.RegisterModule<MusicSourcesStorageModule>();

        builder
            .RegisterType<DebugModuleConfiguration>()
            .As<IModuleConfiguration>()
            .SingleInstance();

        return builder.Build();
    }

    private static IVkApiProvider GetVkApiProviderMock(IVkApi vkApi)
    {
        var mock = new Mock<IVkApiProvider>();
        mock.Setup(x => x.TryProvideAuthorizedVkApi(out vkApi))
            .Returns(true);
        return mock.Object;
    }
}