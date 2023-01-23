using Autofac;
using Module.MusicBee.Autogen;
using Module.MusicBee.Extension.Services;
using Module.MusicBee.Extension.Services.Abstract;

namespace Module.MusicBee.Extension;

public class MusicBeeExtensionModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterModule<MusicBeeAutogenModule>();

        builder
            .RegisterType<ResourceManager>()
            .As<IResourceManager>()
            .SingleInstance();
    }
}