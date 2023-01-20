using Autofac;
using Root.MusicBeeApi;
using Root.MusicBeeApi.Abstract;

namespace MusicBeePlugin
{
    public static class PluginContainer
    {
        public static IContainer GetKernel(MusicBeeApiMemoryContainer mbApiMemoryContainer)
        {
            var builder = new ContainerBuilder();

            builder
                .Register(_ => mbApiMemoryContainer)
                .AsSelf();
            builder
                .RegisterType<MusicBeeApiMemoryContainerWrapper>()
                .As<IMusicBeeApi>()
                .SingleInstance();

            return builder.Build();
        }
    }
}