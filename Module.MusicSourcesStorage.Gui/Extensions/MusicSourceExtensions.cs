using Module.MusicSourcesStorage.Database.Models;
using Module.MusicSourcesStorage.Gui.Enums;

namespace Module.MusicSourcesStorage.Gui.Extensions;

public static class MusicSourceExtensions
{
    public static MusicSourceType GetSourceType(this MusicSource musicSource)
    {
        return musicSource switch
        {
            VkPostWithArchiveSource => MusicSourceType.VkPost,
            TorrentSource => MusicSourceType.Torrent,
            _ => throw new ArgumentOutOfRangeException(
                nameof(musicSource),
                musicSource,
                "Got unknown source type."
            )
        };
    }
}