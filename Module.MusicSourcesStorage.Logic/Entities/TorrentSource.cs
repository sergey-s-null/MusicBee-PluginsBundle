using Module.MusicSourcesStorage.Logic.Enums;

namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed class TorrentSource : MusicSource
{
    /// <summary>
    /// "New" mean that created model does not exists in database yet.
    /// </summary>
    public static TorrentSource New(
        string name,
        IReadOnlyList<SourceFile> files,
        byte[] torrentFile)
    {
        return new TorrentSource(
            0,
            name,
            files,
            torrentFile
        );
    }

    public byte[] TorrentFile { get; }

    public TorrentSource(
        int id,
        string name,
        IReadOnlyList<SourceFile> files,
        byte[] torrentFile)
        : base(id, name, MusicSourceType.Torrent, files)
    {
        TorrentFile = torrentFile;
    }
}