using Module.MusicSourcesStorage.Logic.Enums;

namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed class ImageFileModel : FileModel
{
    /// <summary>
    /// "New" mean that created model does not exists in database yet.
    /// </summary>
    public static ImageFileModel New(string path, long size)
    {
        return new ImageFileModel(
            0,
            path,
            size,
            false,
            null
        );
    }

    public bool IsCover { get; }
    public byte[]? Data { get; }

    public ImageFileModel(
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