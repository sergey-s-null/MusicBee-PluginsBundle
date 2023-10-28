using System.ComponentModel.DataAnnotations;

namespace Module.MusicSourcesStorage.Database.Models;

public abstract class MusicSource
{
    public int Id { get; set; }
    [Required] public string Name { get; set; } = string.Empty;

    public List<File> Files { get; set; } = new();
}