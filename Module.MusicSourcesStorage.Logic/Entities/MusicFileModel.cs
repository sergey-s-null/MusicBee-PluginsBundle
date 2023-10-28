using Module.MusicSourcesStorage.Logic.Enums;

namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed class MusicFileModel : FileModel
{
    /// <summary>
    /// "New" mean that created model does not exists in database yet.
    /// </summary>
    public static MusicFileModel New(string path, long size)
    {
        return new MusicFileModel(
            0,
            path,
            size,
            false
        );
    }

    public bool IsListened { get; }

    public MusicFileModel(
        int id,
        string path,
        long size,
        bool isListened)
        : base(id, path, size, FileType.MusicFile)
    {
        IsListened = isListened;
    }
}