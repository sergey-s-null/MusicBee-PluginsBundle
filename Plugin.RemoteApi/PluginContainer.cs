using Autofac;
using Mead.MusicBee.Api.Autofac.DependencyInjection.Extensions;
using Mead.MusicBee.Services;

namespace MusicBeePlugin
{
    public static class PluginContainer
    {
        public static IContainer Create(MusicBeeApiMemoryContainer mbApiMemoryContainer)
        {
            var builder = new ContainerBuilder();

            builder.RegisterMusicBeeApi(mbApiMemoryContainer);

            return builder.Build();
        }
    }
}