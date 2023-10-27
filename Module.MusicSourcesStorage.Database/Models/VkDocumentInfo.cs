using System.ComponentModel.DataAnnotations.Schema;

namespace Module.MusicSourcesStorage.Database.Models;

[ComplexType]
public sealed class VkDocumentInfo
{
    public long Id { get; set; }
    public long OwnerId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Uri { get; set; } = string.Empty;
    public long? Size { get; set; }
}