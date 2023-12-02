using System.ComponentModel.DataAnnotations.Schema;

namespace Module.MusicSourcesStorage.Database.Models;

[ComplexType]
public sealed class VkPostModel
{
    /// <summary>
    /// Id of post in VK context.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Post owner id in VK context.
    /// </summary>
    public long OwnerId { get; set; }
}