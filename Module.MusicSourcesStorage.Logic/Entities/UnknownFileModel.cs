using Module.MusicSourcesStorage.Logic.Enums;

namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed class UnknownFileModel : FileModel
{
    /// <summary>
    /// "New" mean that created model does not exists in database yet.
    /// </summary>
    public static UnknownFileModel New(string path, long size)
    {
        return new UnknownFileModel(
            0,
            path,
            size
        );
    }

    public UnknownFileModel(
        int id,
        string path,
        long size)
        : base(id, path, size, FileType.Unknown)
    {
    }
}