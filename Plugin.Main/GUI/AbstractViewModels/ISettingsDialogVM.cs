﻿using System.Collections.Generic;
using System.Windows.Input;
using Module.ArtworksSearcher.GUI.Settings;
using Module.PlaylistsExporter.GUI.Settings;
using Module.Vk.GUI.AbstractViewModels;
using Module.VkAudioDownloader.GUI.AbstractViewModels;
using Root.Abstractions;

namespace MusicBeePlugin.GUI.AbstractViewModels
{
    public interface ISettingsDialogVM : ISettings
    {
        IVkSettingsVM VkSettingsVM { get; }
        IMusicDownloaderSettingsVM MusicDownloaderSettingsVM { get; }
        IArtworksSearcherSettingsVM ArtworksSearcherSettingsVM { get; }
        IPlaylistsExporterSettingsVM PlaylistsExporterSettingsVM { get; }

        IList<IModuleSettingsVM> SettingsModules { get; }
        IModuleSettingsVM SelectedSettingsModule { get; set; }
        ICommand ResetCmd { get; }
    }
}