using System.ComponentModel.DataAnnotations;

namespace Module.MusicSourcesStorage.Database.Models;

public abstract class FileModel
{
    public int Id { get; set; }
    [Required] public string Path { get; set; } = string.Empty;
    [Required] public long Size { get; set; }

    [Required] public MusicSourceModel? Source { get; set; }
}