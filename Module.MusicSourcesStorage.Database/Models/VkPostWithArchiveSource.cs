namespace Module.MusicSourcesStorage.Database.Models;

public sealed class VkPostWithArchiveSource : MusicSource
{
    public VkPostInfo PostInfo { get; set; } = new();

    public VkDocumentInfo SelectedDocumentInfo { get; set; } = new();
}