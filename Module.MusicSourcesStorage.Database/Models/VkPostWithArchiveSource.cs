namespace Module.MusicSourcesStorage.Database.Models;

public sealed class VkPostWithArchiveSource : MusicSource
{
    public int PostOwnerId { get; set; }
    public int PostId { get; set; }
    // todo add some information to define what attachment to use
}