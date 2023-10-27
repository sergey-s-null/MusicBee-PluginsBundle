using System.ComponentModel.DataAnnotations;

namespace Module.MusicSourcesStorage.Database.Models;

public abstract class File
{
    public int Id { get; set; }
    [Required] public string Path { get; set; } = string.Empty;
    [Required] public long Size { get; set; }

    [Required] public MusicSource? Source { get; set; }
}