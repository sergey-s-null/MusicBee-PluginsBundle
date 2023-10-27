using System.ComponentModel.DataAnnotations.Schema;

namespace Module.MusicSourcesStorage.Database.Models;

[ComplexType]
public sealed class VkPostInfo
{
    public ulong Id { get; set; }
    public ulong OwnerId { get; set; }
}