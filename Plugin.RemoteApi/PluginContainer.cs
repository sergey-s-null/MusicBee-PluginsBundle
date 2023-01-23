using Autofac;
using Module.MusicBee.Autogen;
using Module.MusicBee.Services;

namespace MusicBeePlugin
{
    public static class PluginContainer
    {
        public static IContainer Create(MusicBeeApiMemoryContainer mbApiMemoryContainer)
        {
            var builder = new ContainerBuilder();

            builder
                .Register(_ => mbApiMemoryContainer)
                .AsSelf();

            builder.RegisterModule<MusicBeeAutogenModule>();

            return builder.Build();
        }
    }
}