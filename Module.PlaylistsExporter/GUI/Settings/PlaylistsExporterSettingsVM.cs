using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Module.PlaylistsExporter.Settings;
using MoreLinq;
using PropertyChanged;
using Root.Exceptions;
using Root.Helpers;
using Root.MusicBeeApi.Abstract;
using Root.MVVM;

namespace Module.PlaylistsExporter.GUI.Settings
{
    [AddINotifyPropertyChangedInterface]
    public class PlaylistsExporterSettingsVM : IPlaylistsExporterSettingsVM
    {
        public bool Loaded { get; private set; }

        public string PlaylistsDirectoryPath { get; set; } = "";
        public string FilesLibraryPath { get; set; } = "";
        public string PlaylistsNewDirectoryName { get; set; } = "";

        public string PlaylistsBasePath { get; set; } = "";
        public IList<PlaylistVM> Playlists { get; } = new ObservableCollection<PlaylistVM>();

        private RelayCommand? _applyCheckStateToSelectedCmd;

        public ICommand ApplyCheckStateToSelectedCmd =>
            _applyCheckStateToSelectedCmd ??= new RelayCommand(arg =>
            {
                var argsArr = (object[]) arg!;
                var triggered = (PlaylistVM) argsArr[0];
                var selectedObjects = (IReadOnlyCollection<object>) argsArr[1];
                var selected = selectedObjects
                    .OfType<PlaylistVM>()
                    .ToReadOnlyCollection();

                ApplyCheckStateToSelected(triggered, selected);
            });

        private readonly IMusicBeeApi _mbApi;
        private readonly IPlaylistsExporterSettings _playlistsExporterSettings;

        public PlaylistsExporterSettingsVM(
            IMusicBeeApi mbApi,
            IPlaylistsExporterSettings playlistsExporterSettings)
        {
            _mbApi = mbApi;
            _playlistsExporterSettings = playlistsExporterSettings;

            SetPlaylistsInfo();
        }

        private void SetPlaylistsInfo()
        {
            var uris = _mbApi.Playlist_QueryPlaylistsEx(out var playlistPaths)
                ? playlistPaths!.Select(x => new Uri(x)).ToReadOnlyList()
                : _playlistsExporterSettings.PlaylistsForExport.Select(x => new Uri(x)).ToReadOnlyList();

            var (common, particulars) = UriHelper.SplitOnCommonAndParticulars(uris);

            PlaylistsBasePath = common.LocalPath;
            particulars
                .Select(x => x.ToLocalOrBackSlashPath())
                .ForEach(x => Playlists.Add(new PlaylistVM(x)
                {
                    Selected = IsPlaylistSelectedInSettings(x)
                }));
        }

        private static void ApplyCheckStateToSelected(PlaylistVM triggered, IReadOnlyCollection<PlaylistVM> selected)
        {
            if (!selected.Contains(triggered))
            {
                return;
            }

            foreach (var vkAudioVM in selected.Where(x => x != triggered))
            {
                vkAudioVM.Selected = triggered.Selected;
            }
        }

        public void Load()
        {
            try
            {
                _playlistsExporterSettings.Load();
                Loaded = true;
            }
            catch (SettingsLoadException)
            {
                // todo display error
                Loaded = false;
            }

            PlaylistsDirectoryPath = _playlistsExporterSettings.PlaylistsDirectoryPath;
            FilesLibraryPath = _playlistsExporterSettings.FilesLibraryPath;
            PlaylistsNewDirectoryName = _playlistsExporterSettings.PlaylistsNewDirectoryName;
            Playlists.ForEach(x => x.Selected = IsPlaylistSelectedInSettings(x.RelativePath));
        }

        public void Save()
        {
            _playlistsExporterSettings.PlaylistsDirectoryPath = PlaylistsDirectoryPath;
            _playlistsExporterSettings.FilesLibraryPath = FilesLibraryPath;
            _playlistsExporterSettings.PlaylistsNewDirectoryName = PlaylistsNewDirectoryName;
            _playlistsExporterSettings.PlaylistsForExport = Playlists
                .Where(x => x.Selected)
                .Select(x => PlaylistsBasePath + x.RelativePath)
                .ToReadOnlyCollection();

            _playlistsExporterSettings.Save();
        }

        private bool IsPlaylistSelectedInSettings(string playlistRelativePath)
        {
            return _playlistsExporterSettings.PlaylistsForExport.Contains(PlaylistsBasePath + playlistRelativePath);
        }
    }
}