namespace Module.PlaylistsExporter.Services;

public interface IPlaylistsExportService
{
    IReadOnlyCollection<string> GetExistingExportedPlaylists();
    void CleanAndExport();
}