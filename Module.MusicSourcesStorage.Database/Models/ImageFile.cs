namespace Module.MusicSourcesStorage.Database.Models;

public sealed class ImageFile : File
{
    public bool IsCover { get; set; }
    public byte[]? Data { get; set; } = Array.Empty<byte>();
}