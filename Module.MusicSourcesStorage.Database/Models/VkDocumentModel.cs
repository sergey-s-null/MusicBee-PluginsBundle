using System.ComponentModel.DataAnnotations.Schema;

namespace Module.MusicSourcesStorage.Database.Models;

[ComplexType]
public sealed class VkDocumentModel
{
    /// <summary>
    /// Document id in VK context.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Document owner id in VK context.
    /// </summary>
    public long OwnerId { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Uri { get; set; } = string.Empty;
    public long? Size { get; set; }
}