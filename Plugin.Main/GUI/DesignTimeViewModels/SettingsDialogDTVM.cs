using Module.ArtworksSearcher.GUI.Settings;
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
    public IVkSettingsVM VkSettingsVM { get; }
    public IMusicDownloaderSettingsVM MusicDownloaderSettingsVM { get; }
    public IArtworksSearcherSettingsVM ArtworksSearcherSettingsVM { get; }
    public IPlaylistsExporterSettingsVM PlaylistsExporterSettingsVM { get; }
        
    public IList<IModuleSettingsVM> SettingsModules { get; }
    public IModuleSettingsVM SelectedSettingsModule { get; set; }

    public SettingsDialogDTVM()
    {
        VkSettingsVM = new VkSettingsDTVM();
        MusicDownloaderSettingsVM = new MusicDownloaderSettingsDTVM();
        ArtworksSearcherSettingsVM = new ArtworksSearcherSettingsDTVM();
        PlaylistsExporterSettingsVM = new PlaylistsExporterSettingsDTVM();
            
        SettingsModules = new List<IModuleSettingsVM>
        {
            new ModuleSettingsVM("Vk", VkSettingsVM),
            new ModuleSettingsVM("Music downloader", MusicDownloaderSettingsVM),
            new ModuleSettingsVM("Artworks searcher", ArtworksSearcherSettingsVM),
            new ModuleSettingsVM("Playlists exporter", PlaylistsExporterSettingsVM),
        };
        SelectedSettingsModule = SettingsModules.First();
    }
}