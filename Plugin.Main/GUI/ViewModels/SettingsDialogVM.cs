using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Module.ArtworksSearcher.GUI.Settings;
using Module.PlaylistsExporter.GUI.Settings;
using Module.Settings.Exceptions;
using Module.Vk.GUI.AbstractViewModels;
using Module.VkAudioDownloader.GUI.AbstractViewModels;
using MusicBeePlugin.GUI.AbstractViewModels;
using PropertyChanged;

namespace MusicBeePlugin.GUI.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class SettingsDialogVM : ISettingsDialogVM
    {
        public IVkSettingsVM VkSettingsVM { get; }
        public IMusicDownloaderSettingsVM MusicDownloaderSettingsVM { get; }
        public IArtworksSearcherSettingsVM ArtworksSearcherSettingsVM { get; }
        public IPlaylistsExporterSettingsVM PlaylistsExporterSettingsVM { get; }

        public IList<IModuleSettingsVM> SettingsModules { get; } = new ObservableCollection<IModuleSettingsVM>();

        public IModuleSettingsVM SelectedSettingsModule { get; set; }

        public SettingsDialogVM(
            IVkSettingsVM vkSettingsVM,
            IMusicDownloaderSettingsVM musicDownloaderSettingsVM,
            IArtworksSearcherSettingsVM artworksSearcherSettingsVM,
            IPlaylistsExporterSettingsVM playlistsExporterSettingsVM)
        {
            VkSettingsVM = vkSettingsVM;
            MusicDownloaderSettingsVM = musicDownloaderSettingsVM;
            ArtworksSearcherSettingsVM = artworksSearcherSettingsVM;
            PlaylistsExporterSettingsVM = playlistsExporterSettingsVM;

            SettingsModules.Add(new ModuleSettingsVM("Vk",
                vkSettingsVM));
            SettingsModules.Add(new ModuleSettingsVM("Music downloader",
                musicDownloaderSettingsVM));
            SettingsModules.Add(new ModuleSettingsVM("Artworks searcher",
                artworksSearcherSettingsVM));
            SettingsModules.Add(new ModuleSettingsVM("Playlists exporter",
                playlistsExporterSettingsVM));

            SelectedSettingsModule = SettingsModules.First();

            Load();
        }

        private void Load()
        {
            foreach (var moduleSettingsVM in SettingsModules)
            {
                moduleSettingsVM.ModuleSettings.Load();
            }
        }

        /// <returns>true - settings window can be closed. false - continue edit settings.</returns>
        public bool Save()
        {
            foreach (var setting in SettingsModules)
            {
                try
                {
                    if (!setting.ModuleSettings.Save())
                    {
                        return false;
                    }
                }
                catch (SettingsSaveException e)
                {
                    var dialogResult = MessageBox.Show(
                        $"Error on save settings module \"{setting.ModuleName}\".\n\n" +
                        $"{e}\n\n" +
                        "Continue save other settings modules?",
                        "Error!",
                        MessageBoxButton.YesNo
                    );
                    if (dialogResult != MessageBoxResult.Yes)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}