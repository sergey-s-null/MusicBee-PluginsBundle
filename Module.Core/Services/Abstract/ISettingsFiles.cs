namespace Module.Core.Services.Abstract;

public interface ISettingsFiles
{
    string ArtworksSearcherSettingsFilePath { get; }
    string PlaylistExporterSettingsFilePath { get; }
    string VkSettingsFilePath { get; }
    string AudioDownloaderSettingsFilePath { get; }
}