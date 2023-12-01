using System.IO;
using Module.Settings.Core;
using Plugin.Main.Configurations.Abstract;

namespace Plugin.Main.Configurations;

public sealed class SettingsConfiguration : IModuleConfiguration
{
    public string DatabaseConnectionString => GetInitializedDatabaseConnectionString();

    private readonly IPluginConfiguration _pluginConfiguration;
    private readonly Lazy<string> _modulePersistentStoragePath;
    private readonly Lazy<string> _databaseConnectionString;

    public SettingsConfiguration(IPluginConfiguration pluginConfiguration)
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
        return "Data Source=(LocalDb)\\MSSQLLocalDB;" +
               "Initial Catalog=Settings;" +
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
            "Settings"
        );
    }
}