using Module.MusicSourcesStorage.Database.Models;
using Module.MusicSourcesStorage.Gui.Enums;

namespace Module.MusicSourcesStorage.Gui.Extensions;

public static class MusicSourceExtensions
{
    public static MusicSourceType GetSourceType(this MusicSourceModel musicSource)
    {
        return musicSource switch
        {
            VkPostWithArchiveSourceModel => MusicSourceType.VkPost,
            TorrentSourceModel => MusicSourceType.Torrent,
            _ => throw new ArgumentOutOfRangeException(
                nameof(musicSource),
                musicSource,
                "Got unknown source type."
            )
        };
    }
}