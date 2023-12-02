namespace Module.MusicSourcesStorage.Database.Models;

public sealed class ImageFileModel : FileModel
{
    public bool IsCover { get; set; }
    public byte[]? Data { get; set; } = Array.Empty<byte>();
}