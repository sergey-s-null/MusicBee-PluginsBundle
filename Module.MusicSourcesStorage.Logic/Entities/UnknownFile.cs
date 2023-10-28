using Module.MusicSourcesStorage.Logic.Enums;

namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed class UnknownFile : SourceFile
{
    /// <summary>
    /// "New" mean that created model does not exists in database yet.
    /// </summary>
    public static UnknownFile New(string path, long size)
    {
        return new UnknownFile(
            0,
            path,
            size
        );
    }

    public UnknownFile(
        int id,
        string path,
        long size)
        : base(id, path, size, FileType.Unknown)
    {
    }
}