using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Module.MusicSourcesStorage.Database.Models;

public abstract class FileModel
{
    public int Id { get; set; }
    [Required] public string Path { get; set; } = string.Empty;
    [Required] public long Size { get; set; }

    public int SourceId { get; set; }

    [Required, ForeignKey(nameof(SourceId))]
    public MusicSourceModel? Source { get; set; }
}