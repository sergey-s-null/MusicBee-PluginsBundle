using Module.MusicSourcesStorage.Gui.Entities.Abstract;
using Module.MusicSourcesStorage.Gui.Exceptions;

namespace Module.MusicSourcesStorage.Gui.Extensions;

public static class TorrentFileContextExtensions
{
    public static void ValidateHasTorrentFilePath(this ITorrentFileContext context)
    {
        if (context.TorrentFilePath is null)
        {
            throw new ValidationException("Context has not torrent file path.");
        }
    }
}