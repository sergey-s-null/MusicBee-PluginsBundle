using System.ComponentModel.DataAnnotations;

namespace Module.MusicSourcesStorage.Database.Models;

public abstract class MusicSourceModel
{
    public int Id { get; set; }
    [Required] public string Name { get; set; } = string.Empty;

    public List<FileModel> Files { get; set; } = new();
}