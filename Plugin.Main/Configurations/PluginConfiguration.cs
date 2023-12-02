using System.IO;
using Mead.MusicBee.Api.Services.Abstract;
using Plugin.Main.Configurations.Abstract;

namespace Plugin.Main.Configurations;

public sealed class PluginConfiguration : IPluginConfiguration
{
    public string DataSource => "(LocalDb)\\MusicBeePlugins";
    public string PersistentStoragePath => _persistentStoragePath.Value;

    private readonly Lazy<string> _persistentStoragePath;

    public PluginConfiguration(IMusicBeeApi musicBeeApi)
    {
        _persistentStoragePath = new Lazy<string>(() => Path.Combine(
            musicBeeApi.Setting_GetPersistentStoragePath(),
            "s.s.d"
        ));
    }
}