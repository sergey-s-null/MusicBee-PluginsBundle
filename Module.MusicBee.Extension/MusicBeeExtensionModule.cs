using Autofac;
using Mead.MusicBee.Api.Autofac.DependencyInjection.Extensions;

namespace Module.MusicBee.Extension;

public sealed class MusicBeeExtensionModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterMusicBeeApi();
    }
}