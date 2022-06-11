using System.Collections.Generic;
using Module.ArtworksSearcher.GUI.Settings;
using Module.PlaylistsExporter.GUI.Settings;
using Module.Vk.GUI.AbstractViewModels;
using Module.VkAudioDownloader.GUI.AbstractViewModels;

namespace MusicBeePlugin.GUI.AbstractViewModels
{
    public interface ISettingsDialogVM
    {
        IVkSettingsVM VkSettingsVM { get; }
        IMusicDownloaderSettingsVM MusicDownloaderSettingsVM { get; }
        IArtworksSearcherSettingsVM ArtworksSearcherSettingsVM { get; }
        IPlaylistsExporterSettingsVM PlaylistsExporterSettingsVM { get; }

        IList<IModuleSettingsVM> SettingsModules { get; }
        IModuleSettingsVM SelectedSettingsModule { get; set; }
    }
}