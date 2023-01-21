using Autofac;
using Module.MusicBee.Services;
using Module.MusicBee.Services.Abstract;

namespace Module.MusicBee;

public class MusicBeeModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterType<MusicBeeApiMemoryContainerWrapper>()
            .As<IMusicBeeApi>()
            .SingleInstance();
    }
}