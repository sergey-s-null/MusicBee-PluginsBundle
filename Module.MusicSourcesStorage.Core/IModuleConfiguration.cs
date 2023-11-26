namespace Module.MusicSourcesStorage.Core;

public interface IModuleConfiguration
{
    string DatabaseConnectionString { get; }
    string SettingsArea { get; }
}