namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed class TorrentSourceModel : MusicSourceModel
{
    /// <summary>
    /// "New" mean that created model does not exists in database yet.
    /// </summary>
    public static TorrentSourceModel New(
        string name,
        IReadOnlyList<FileModel> files,
        byte[] torrentFile)
    {
        return new TorrentSourceModel(
            0,
            name,
            files,
            torrentFile
        );
    }

    public byte[] TorrentFile { get; }

    public TorrentSourceModel(
        int id,
        string name,
        IReadOnlyList<FileModel> files,
        byte[] torrentFile)
        : base(id, name, files)
    {
        TorrentFile = torrentFile;
    }
}