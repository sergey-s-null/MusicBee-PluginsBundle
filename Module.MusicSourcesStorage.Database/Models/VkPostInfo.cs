using System.ComponentModel.DataAnnotations.Schema;

namespace Module.MusicSourcesStorage.Database.Models;

[ComplexType]
public sealed class VkPostInfo
{
    /// <summary>
    /// Id of post in VK context.
    /// </summary>
    public ulong Id { get; set; }

    /// <summary>
    /// Post owner id in VK context.
    /// </summary>
    public ulong OwnerId { get; set; }
}