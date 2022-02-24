using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Module.PlaylistsExporter.Settings;
using MoreLinq;
using PropertyChanged;
using Root.Helpers;
using Root.MusicBeeApi;
using Root.MVVM;

namespace Module.PlaylistsExporter.GUI.Settings
{
    [AddINotifyPropertyChangedInterface]
    public class PlaylistsExporterSettingsVM : IPlaylistsExporterSettingsVM
    {
        public string PlaylistsDirectoryPath { get; set; } = "";
        public string FilesLibraryPath { get; set; } = "";
        public string PlaylistsNewDirectoryName { get; set; } = "";

        public string PlaylistsBasePath { get; set; } = "";
        public ObservableCollection<PlaylistVM> Playlists { get; } = new();

        private RelayCommand? _applyCheckStateToSelectedCmd;
        public RelayCommand ApplyCheckStateToSelectedCmd =>
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

        public bool IsLoaded => _settings.IsLoaded;

        private readonly MusicBeeApiMemoryContainer _mbApi;
        private readonly IPlaylistsExporterSettings _settings;
        
        public PlaylistsExporterSettingsVM(
            MusicBeeApiMemoryContainer mbApi, 
            IPlaylistsExporterSettings settings)
        {
            _mbApi = mbApi;
            _settings = settings;

            SetPlaylistsInfo();
        }

        private void SetPlaylistsInfo()
        {
            var uris = _mbApi.Playlist_QueryPlaylistsEx(out var playlistPaths)
                ? playlistPaths!.Select(x => new Uri(x)).ToReadOnlyList()
                : _settings.PlaylistsForExport.Select(x => new Uri(x)).ToReadOnlyList();
            
            var (common, particulars) = UriHelper.SplitOnCommonAndParticulars(uris);
            
            PlaylistsBasePath = common.LocalPath;
            particulars
                .Select(x => x.ToLocalOrBackSlashPath())
                .ForEach(x => Playlists.Add(new PlaylistVM
                {
                    Selected = IsPlaylistSelectedInSettings(x),
                    RelativePath = x
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
            _settings.Load();
            
            Reset();
        }

        public bool Save()
        {
            _settings.PlaylistsDirectoryPath = PlaylistsDirectoryPath;
            _settings.FilesLibraryPath = FilesLibraryPath;
            _settings.PlaylistsNewDirectoryName = PlaylistsNewDirectoryName;
            _settings.PlaylistsForExport = Playlists
                .Where(x => x.Selected)
                .Select(x => PlaylistsBasePath + x.RelativePath)
                .ToReadOnlyCollection();

            return _settings.Save();
        }

        public void Reset()
        {
            PlaylistsDirectoryPath = _settings.PlaylistsDirectoryPath;
            FilesLibraryPath = _settings.FilesLibraryPath;
            PlaylistsNewDirectoryName = _settings.PlaylistsNewDirectoryName;
            Playlists.ForEach(x => x.Selected = IsPlaylistSelectedInSettings(x.RelativePath));
        }

        private bool IsPlaylistSelectedInSettings(string playlistRelativePath)
        {
            return _settings.PlaylistsForExport.Contains(PlaylistsBasePath + playlistRelativePath);
        }
    }
}