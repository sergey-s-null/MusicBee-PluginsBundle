using Module.ArtworksSearcher.GUI.Settings;
using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.PlaylistsExporter.GUI.Settings;
using Module.Vk.GUI.AbstractViewModels;
using Module.VkAudioDownloader.GUI.AbstractViewModels;

namespace Plugin.Main.GUI.AbstractViewModels;

public interface ISettingsDialogVM
{
    // todo use ViewMapper
    IVkSettingsVM VkSettings { get; }
    IMusicDownloaderSettingsVM MusicDownloaderSettings { get; }
    IArtworksSearcherSettingsVM ArtworksSearcherSettings { get; }
    IPlaylistsExporterSettingsVM PlaylistsExporterSettings { get; }
    IMusicSourcesStorageSettingsVM MusicSourcesStorageSettings { get; }

    IList<IModuleSettingsVM> SettingsModules { get; }
    IModuleSettingsVM SelectedSettingsModule { get; set; }
}