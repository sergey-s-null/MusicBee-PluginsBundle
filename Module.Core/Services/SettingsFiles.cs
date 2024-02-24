using System.IO;
using Module.Core.Services.Abstract;

namespace Module.Core.Services;

public sealed class SettingsFiles : ISettingsFiles
{
    public string ArtworksSearcherSettingsFilePath => _artworksSearcherSettingsFilePath.Value;
    public string PlaylistExporterSettingsFilePath => _playlistExporterSettingsFilePath.Value;
    public string VkSettingsFilePath => _vkSettingsFilePath.Value;
    public string AudioDownloaderSettingsFilePath => _audioDownloaderSettingsFilePath.Value;

    private readonly IPluginConfiguration _pluginConfiguration;

    private readonly Lazy<string> _artworksSearcherSettingsFilePath;
    private readonly Lazy<string> _playlistExporterSettingsFilePath;
    private readonly Lazy<string> _vkSettingsFilePath;
    private readonly Lazy<string> _audioDownloaderSettingsFilePath;

    public SettingsFiles(IPluginConfiguration pluginConfiguration)
    {
        _pluginConfiguration = pluginConfiguration;

        _artworksSearcherSettingsFilePath = new Lazy<string>(
            () => GetFilePath("ArtworksSearcher/settings.json")
        );
        _playlistExporterSettingsFilePath = new Lazy<string>(
            () => GetFilePath("PlaylistsExporter/settings.json")
        );
        _vkSettingsFilePath = new Lazy<string>(
            () => GetFilePath("Vk/settings.json")
        );
        _audioDownloaderSettingsFilePath = new Lazy<string>(
            () => GetFilePath("AudioDownloader/settings.json")
        );
    }

    private string GetFilePath(string fileRelativePath)
    {
        return Path.Combine(_pluginConfiguration.PersistentStoragePath, fileRelativePath);
    }
}