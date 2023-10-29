using Module.MusicSourcesStorage.Logic.Enums;

namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed class TorrentSource : MusicSource
{
    /// <summary>
    /// "New" mean that created model does not exists in database yet.
    /// </summary>
    public static TorrentSource New(
        MusicSourceAdditionalInfo additionalInfo,
        IReadOnlyList<SourceFile> files,
        byte[] torrentFile)
    {
        return new TorrentSource(
            0,
            additionalInfo,
            files,
            torrentFile
        );
    }

    public byte[] TorrentFile { get; }

    public TorrentSource(
        int id,
        MusicSourceAdditionalInfo additionalInfo,
        IReadOnlyList<SourceFile> files,
        byte[] torrentFile)
        : base(id, additionalInfo, MusicSourceType.Torrent, files)
    {
        TorrentFile = torrentFile;
    }
}