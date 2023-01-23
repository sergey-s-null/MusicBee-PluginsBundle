using Autofac;
using Module.MusicBee.Autogen.Services;
using Module.MusicBee.Autogen.Services.Abstract;

namespace Module.MusicBee.Autogen;

public sealed class MusicBeeAutogenModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterType<MusicBeeApiMemoryContainerWrapper>()
            .As<IMusicBeeApi>()
            .SingleInstance();
    }
}