using Module.ArtworksSearcher.GUI.Settings;
using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.PlaylistsExporter.GUI.Settings;
using Module.Vk.GUI.AbstractViewModels;
using Module.VkAudioDownloader.GUI.AbstractViewModels;

namespace Plugin.Main.GUI.AbstractViewModels;

public interface ISettingsDialogVM
{
    IVkSettingsVM VkSettingsVM { get; }
    IMusicDownloaderSettingsVM MusicDownloaderSettingsVM { get; }
    IArtworksSearcherSettingsVM ArtworksSearcherSettingsVM { get; }
    IPlaylistsExporterSettingsVM PlaylistsExporterSettingsVM { get; }
    IMusicSourcesStorageSettingsVM MusicSourcesStorageSettingsVM { get; }

    IList<IModuleSettingsVM> SettingsModules { get; }
    IModuleSettingsVM SelectedSettingsModule { get; set; }
}