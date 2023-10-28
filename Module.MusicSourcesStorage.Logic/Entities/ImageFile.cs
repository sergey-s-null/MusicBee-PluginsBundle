using Module.MusicSourcesStorage.Logic.Enums;

namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed class ImageFile : SourceFile
{
    /// <summary>
    /// "New" mean that created model does not exists in database yet.
    /// </summary>
    public static ImageFile New(string path, long size)
    {
        return new ImageFile(
            0,
            path,
            size,
            false,
            null
        );
    }

    public bool IsCover { get; }
    public byte[]? Data { get; }

    public ImageFile(
        int id,
        string path,
        long size,
        bool isCover,
        byte[]? data)
        : base(id, path, size, FileType.Image)
    {
        IsCover = isCover;
        Data = data;
    }
}