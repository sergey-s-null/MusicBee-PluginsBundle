using Module.Settings.Core;
using Module.Settings.Database.Services.Abstract;

namespace Module.Settings.Database.Services;

public sealed class SettingsContextFactory : ISettingsContextFactory
{
    private readonly Func<IModuleConfiguration> _configurationFactory;

    public SettingsContextFactory(Func<IModuleConfiguration> configurationFactory)
    {
        _configurationFactory = configurationFactory;
    }

    public SettingsContext Create()
    {
        return new SettingsContext(_configurationFactory());
    }
}