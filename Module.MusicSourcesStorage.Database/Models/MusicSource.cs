using System.ComponentModel.DataAnnotations;

namespace Module.MusicSourcesStorage.Database.Models;

public abstract class MusicSource
{
    public int Id { get; set; }
    [Required] public string Name { get; set; } = null!;

    public IList<File> Files { get; set; } = null!;
}