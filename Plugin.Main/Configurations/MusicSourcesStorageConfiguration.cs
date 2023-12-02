using System.IO;
using Module.MusicSourcesStorage.Core;
using Plugin.Main.Configurations.Abstract;

namespace Plugin.Main.Configurations;

public sealed class MusicSourcesStorageConfiguration : IModuleConfiguration
{
    public string DatabaseConnectionString => GetInitializedDatabaseConnectionString();
    public string SettingsArea => "music-sources-storage";

    private readonly IPluginConfiguration _pluginConfiguration;
    private readonly Lazy<string> _modulePersistentStoragePath;
    private readonly Lazy<string> _databaseConnectionString;

    public MusicSourcesStorageConfiguration(IPluginConfiguration pluginConfiguration)
    {
        _pluginConfiguration = pluginConfiguration;

        _modulePersistentStoragePath = new Lazy<string>(GetModulePersistentStoragePath);
        _databaseConnectionString = new Lazy<string>(GetDatabaseConnectionString);
    }

    private string GetInitializedDatabaseConnectionString()
    {
        if (!Directory.Exists(_modulePersistentStoragePath.Value))
        {
            Directory.CreateDirectory(_modulePersistentStoragePath.Value);
        }

        return _databaseConnectionString.Value;
    }

    private string GetDatabaseConnectionString()
    {
        return $"Data Source={_pluginConfiguration.DataSource};" +
               "Initial Catalog=MusicSourcesStorage;" +
               $"AttachDBFilename={GetDatabaseFilePath()};";
    }

    private string GetDatabaseFilePath()
    {
        return Path.Combine(
            _modulePersistentStoragePath.Value,
            "data.mdf"
        );
    }

    private string GetModulePersistentStoragePath()
    {
        return Path.Combine(
            _pluginConfiguration.PersistentStoragePath,
            "MusicSourcesStorage"
        );
    }
}