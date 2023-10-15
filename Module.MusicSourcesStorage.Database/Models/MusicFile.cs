namespace Module.MusicSourcesStorage.Database.Models;

public sealed class MusicFile : File
{
    public bool IsListened { get; set; }
}