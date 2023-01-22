using Autofac;
using Module.MusicBee.Autogen.Services.Abstract;
using Module.MusicBee.Services;

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