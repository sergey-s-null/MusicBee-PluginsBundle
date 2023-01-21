using Autofac;
using Module.MusicBee.Services;
using Module.MusicBee.Services.Abstract;

namespace Module.MusicBee;

public class MusicBeeModule : Autofac.Module
{
    private readonly MusicBeeApiMemoryContainer _musicBeeApiMemoryContainer;

    public MusicBeeModule(MusicBeeApiMemoryContainer musicBeeApiMemoryContainer)
    {
        _musicBeeApiMemoryContainer = musicBeeApiMemoryContainer;
    }

    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterType<MusicBeeApiMemoryContainerWrapper>()
            .As<IMusicBeeApi>()
            .SingleInstance();
        builder
            .Register(_ => _musicBeeApiMemoryContainer)
            .AsSelf()
            .SingleInstance();
    }
}