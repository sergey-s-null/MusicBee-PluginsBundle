using Module.MusicSourcesStorage.Gui.Entities.Abstract;
using Module.MusicSourcesStorage.Logic.Entities;

namespace Module.MusicSourcesStorage.Gui.Entities;

public sealed class AddingVkPostWithArchiveContext : IAddingVkPostWithArchiveContext
{
    public string? Error { get; set; }

    public VkPostGlobalId? PostId { get; set; }
    public IReadOnlyList<VkDocumentModel>? AttachedDocuments { get; set; }
    public VkDocumentModel? SelectedDocument { get; set; }
    public IReadOnlyList<IndexedFile>? IndexedFiles { get; set; }
}