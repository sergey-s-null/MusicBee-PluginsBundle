using System.Collections.Generic;

namespace Root.Helpers
{
    public static class MusicBeeApiInterfaceHelper
    {
        public static bool Playlist_QueryPlaylistsEx(
            this MusicBeeApiInterface mbApi, 
            out IReadOnlyCollection<string> playlists)
        {
            if (!mbApi.Playlist_QueryPlaylists())
            {
                playlists = null;
                return false;
            }

            var result = new List<string>();
            while (true)
            {
                var next = mbApi.Playlist_QueryGetNextPlaylist();
                if (string.IsNullOrEmpty(next))
                {
                    break;
                }
                result.Add(next);
            }

            playlists = result;
            return true;
        }
    }
}