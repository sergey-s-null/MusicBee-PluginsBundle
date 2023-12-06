using Module.MusicSourcesStorage.Core;
using Module.MusicSourcesStorage.Database.Factories.Abstract;

namespace Module.MusicSourcesStorage.Database.Factories;

public sealed class MusicSourcesStorageContextFactory : IMusicSourcesStorageContextFactory
{
    private readonly Func<IModuleConfiguration> _moduleConfigurationFactory;

    public MusicSourcesStorageContextFactory(Func<IModuleConfiguration> moduleConfigurationFactory)
    {
        _moduleConfigurationFactory = moduleConfigurationFactory;
    }

    public MusicSourcesStorageContext Create()
    {
        return new MusicSourcesStorageContext(_moduleConfigurationFactory());
    }
}