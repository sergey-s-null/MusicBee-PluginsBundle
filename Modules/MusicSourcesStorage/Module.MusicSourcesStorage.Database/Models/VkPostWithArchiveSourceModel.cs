namespace Module.MusicSourcesStorage.Database.Models;

public sealed class VkPostWithArchiveSourceModel : MusicSourceModel
{
    public VkPostModel Post { get; set; } = new();

    public VkDocumentModel Document { get; set; } = new();
}