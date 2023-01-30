using Autofac;
using Mead.MusicBee.Api.Autofac.DependencyInjection.Extensions;
using Module.MusicBee.Extension.Services;
using Module.MusicBee.Extension.Services.Abstract;

namespace Module.MusicBee.Extension;

public class MusicBeeExtensionModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterMusicBeeApi();

        builder
            .RegisterType<ResourceManager>()
            .As<IResourceManager>()
            .SingleInstance();
    }
}