using Module.PlaylistsExporter.Entities;

namespace Module.PlaylistsExporter.Services
{
    public interface IPlaylistToLibraryConverter
    {
        Playlist Convert(Playlist playlist);
    }
}