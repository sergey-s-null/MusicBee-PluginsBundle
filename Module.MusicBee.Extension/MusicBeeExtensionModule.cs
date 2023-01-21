using Autofac;
using Module.MusicBee.Extension.Services;
using Module.MusicBee.Extension.Services.Abstract;

namespace Module.MusicBee.Extension;

public class MusicBeeExtensionModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterModule<MusicBeeModule>();

        builder
            .RegisterType<ResourceManager>()
            .As<IResourceManager>()
            .SingleInstance();
    }
}