using System.Collections.Generic;
using System.Windows.Input;
using Module.ArtworksSearcher.GUI.Settings;
using Module.PlaylistsExporter.GUI.Settings;
using Module.VkAudioDownloader.GUI.AbstractViewModels;
using Root.Abstractions;

namespace MusicBeePlugin.GUI.SettingsDialog
{
    public interface ISettingsDialogVM : ISettings
    {
        IMusicDownloaderSettingsVM MusicDownloaderSettingsVM { get; }
        IArtworksSearcherSettingsVM ArtworksSearcherSettingsVM { get; }
        IPlaylistsExporterSettingsVM PlaylistsExporterSettingsVM { get; }

        IList<IModuleSettingsVM> SettingsModules { get; }
        IModuleSettingsVM SelectedSettingsModule { get; set; }
        ICommand ResetCmd { get; }
    }
}