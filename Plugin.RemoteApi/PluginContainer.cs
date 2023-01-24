using Autofac;
using Mead.MusicBee.Services;
using Module.MusicBee.Autogen;

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