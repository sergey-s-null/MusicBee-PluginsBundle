using System;
using System.Collections.Generic;
using System.Windows.Input;
using Module.Mvvm.Extension;

namespace Module.PlaylistsExporter.GUI.Settings
{
    public sealed class PlaylistsExporterSettingsDTVM : IPlaylistsExporterSettingsVM
    {
        public bool Loaded => false;
        public string LoadingErrorMessage => "Another error.";

        public string PlaylistsDirectoryPath { get; set; } = @"E:\Path\To\Playlists Directory";
        public string FilesLibraryPath { get; set; } = @"E:\Library\Path";
        public string PlaylistsNewDirectoryName { get; set; } = @"DirectoryName";
        public string PlaylistsBasePath => @"E:\Base\Path";
        public IList<PlaylistVM> Playlists { get; }

        public ICommand ApplyCheckStateToSelectedCmd { get; } =
            new RelayCommand(_ => throw new NotSupportedException());

        public ICommand ReloadCmd { get; } = RelayCommand.Empty;

        public PlaylistsExporterSettingsDTVM()
        {
            Playlists = new[]
            {
                new PlaylistVM("Most played") { Selected = true },
                new PlaylistVM("Playlist 1"),
                new PlaylistVM("Playlist 2"),
                new PlaylistVM("No rating"),
                new PlaylistVM("Playlist 3"),
                new PlaylistVM("Playlist 4") { Selected = true },
                new PlaylistVM("Playlist 5"),
                new PlaylistVM("Unknown"),
                new PlaylistVM("Unknown"),
                new PlaylistVM("Unknown"),
                new PlaylistVM("Unknown"),
                new PlaylistVM("Unknown"),
                new PlaylistVM("Unknown"),
                new PlaylistVM("Unknown"),
                new PlaylistVM("Unknown"),
                new PlaylistVM("Unknown"),
                new PlaylistVM("Unknown"),
                new PlaylistVM("Unknown"),
                new PlaylistVM("Unknown"),
                new PlaylistVM("Unknown"),
                new PlaylistVM("Unknown"),
                new PlaylistVM("Unknown"),
                new PlaylistVM("Unknown"),
                new PlaylistVM("Unknown"),
                new PlaylistVM("Unknown"),
                new PlaylistVM("Unknown"),
                new PlaylistVM("Unknown"),
                new PlaylistVM("Unknown"),
                new PlaylistVM("Unknown"),
                new PlaylistVM("Unknown"),
                new PlaylistVM("Unknown"),
                new PlaylistVM("Unknown"),
                new PlaylistVM("Unknown"),
                new PlaylistVM("Unknown"),
                new PlaylistVM("Unknown"),
                new PlaylistVM("Unknown"),
                new PlaylistVM("Unknown"),
                new PlaylistVM("Unknown"),
                new PlaylistVM("Unknown"),
                new PlaylistVM("Unknown"),
                new PlaylistVM("Unknown"),
                new PlaylistVM("Unknown"),
            };
        }

        public void Load()
        {
            throw new NotSupportedException();
        }

        public bool Save()
        {
            throw new NotSupportedException();
        }
    }
}