using Module.ArtworksSearcher.GUI.Settings;
using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Gui.DesignTimeViewModels;
using Module.PlaylistsExporter.GUI.Settings;
using Module.Vk.GUI.AbstractViewModels;
using Module.Vk.GUI.DesignTimeViewModels;
using Module.VkAudioDownloader.GUI.AbstractViewModels;
using Module.VkAudioDownloader.GUI.DesignTimeViewModels;
using Plugin.Main.GUI.AbstractViewModels;
using Plugin.Main.GUI.ViewModels;
using PropertyChanged;

namespace Plugin.Main.GUI.DesignTimeViewModels;

[AddINotifyPropertyChangedInterface]
public sealed class SettingsDialogDTVM : ISettingsDialogVM
{
    public IVkSettingsVM VkSettings { get; }
    public IMusicDownloaderSettingsVM MusicDownloaderSettings { get; }
    public IArtworksSearcherSettingsVM ArtworksSearcherSettings { get; }
    public IPlaylistsExporterSettingsVM PlaylistsExporterSettings { get; }
    public IMusicSourcesStorageSettingsVM MusicSourcesStorageSettings { get; }

    public IList<IModuleSettingsVM> SettingsModules { get; }
    public IModuleSettingsVM SelectedSettingsModule { get; set; }

    public SettingsDialogDTVM()
    {
        VkSettings = new VkSettingsDTVM();
        MusicDownloaderSettings = new MusicDownloaderSettingsDTVM();
        ArtworksSearcherSettings = new ArtworksSearcherSettingsDTVM();
        PlaylistsExporterSettings = new PlaylistsExporterSettingsDTVM();
        MusicSourcesStorageSettings = new MusicSourcesStorageSettingsDTVM();

        SettingsModules = new List<IModuleSettingsVM>
        {
            new ModuleSettingsVM("Vk", VkSettings),
            new ModuleSettingsVM("Music downloader", MusicDownloaderSettings),
            new ModuleSettingsVM("Artworks searcher", ArtworksSearcherSettings),
            new ModuleSettingsVM("Playlists exporter", PlaylistsExporterSettings),
            new ModuleSettingsVM("Music sources", MusicSourcesStorageSettings),
        };
        SelectedSettingsModule = SettingsModules.First();
    }
}