using Module.PlaylistsExporter.Settings;

namespace Module.PlaylistsExporter.GUI.Settings
{
    public class PlaylistsExporterSettingsVM : IPlaylistsExporterSettingsVM
    {
        public string PlaylistsDirectoryPath { get; set; } = "";
        public string FilesLibraryPath { get; set; } = "";
        public string PlaylistsNewDirectoryName { get; set; } = "";

        public bool IsLoaded => _settings.IsLoaded;

        private readonly IPlaylistsExporterSettings _settings;
        
        public PlaylistsExporterSettingsVM(IPlaylistsExporterSettings settings)
        {
            _settings = settings;
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

            return _settings.Save();
        }

        public void Reset()
        {
            PlaylistsDirectoryPath = _settings.PlaylistsDirectoryPath;
            FilesLibraryPath = _settings.FilesLibraryPath;
            PlaylistsNewDirectoryName = _settings.PlaylistsNewDirectoryName;
        }

    }
}