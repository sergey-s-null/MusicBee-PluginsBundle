using Module.MusicSourcesStorage.Logic.Enums;

namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed class MusicFile : SourceFile
{
    /// <summary>
    /// "New" mean that created model does not exists in database yet.
    /// </summary>
    public static MusicFile New(string path, long size)
    {
        return new MusicFile(
            0,
            0,
            path,
            size,
            false
        );
    }

    public bool IsListened { get; }

    public MusicFile(
        int id,
        int sourceId,
        string path,
        long size,
        bool isListened)
        : base(id, sourceId, path, size, FileType.MusicFile)
    {
        IsListened = isListened;
    }
}