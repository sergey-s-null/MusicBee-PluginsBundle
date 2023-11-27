using System.Collections.ObjectModel;
using System.Windows;
using Module.ArtworksSearcher.GUI.Settings;
using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.PlaylistsExporter.GUI.Settings;
using Module.Settings.Exceptions;
using Module.Vk.GUI.AbstractViewModels;
using Module.VkAudioDownloader.GUI.AbstractViewModels;
using Plugin.Main.GUI.AbstractViewModels;
using PropertyChanged;

namespace Plugin.Main.GUI.ViewModels;

[AddINotifyPropertyChangedInterface]
public sealed class SettingsDialogVM : ISettingsDialogVM
{
    public IVkSettingsVM VkSettingsVM { get; }
    public IMusicDownloaderSettingsVM MusicDownloaderSettingsVM { get; }
    public IArtworksSearcherSettingsVM ArtworksSearcherSettingsVM { get; }
    public IPlaylistsExporterSettingsVM PlaylistsExporterSettingsVM { get; }
    public IMusicSourcesStorageSettingsVM MusicSourcesStorageSettingsVM { get; }

    public IList<IModuleSettingsVM> SettingsModules { get; } = new ObservableCollection<IModuleSettingsVM>();

    public IModuleSettingsVM SelectedSettingsModule { get; set; }

    public SettingsDialogVM(
        IVkSettingsVM vkSettingsVM,
        IMusicDownloaderSettingsVM musicDownloaderSettingsVM,
        IArtworksSearcherSettingsVM artworksSearcherSettingsVM,
        IPlaylistsExporterSettingsVM playlistsExporterSettingsVM,
        IMusicSourcesStorageSettingsVM musicSourcesStorageSettingsVM)
    {
        VkSettingsVM = vkSettingsVM;
        MusicDownloaderSettingsVM = musicDownloaderSettingsVM;
        ArtworksSearcherSettingsVM = artworksSearcherSettingsVM;
        PlaylistsExporterSettingsVM = playlistsExporterSettingsVM;
        MusicSourcesStorageSettingsVM = musicSourcesStorageSettingsVM;

        SettingsModules.Add(new ModuleSettingsVM("Vk",
            vkSettingsVM));
        SettingsModules.Add(new ModuleSettingsVM("Music downloader",
            musicDownloaderSettingsVM));
        SettingsModules.Add(new ModuleSettingsVM("Artworks searcher",
            artworksSearcherSettingsVM));
        SettingsModules.Add(new ModuleSettingsVM("Playlists exporter",
            playlistsExporterSettingsVM));
        SettingsModules.Add(new ModuleSettingsVM("Music sources",
            musicSourcesStorageSettingsVM));

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