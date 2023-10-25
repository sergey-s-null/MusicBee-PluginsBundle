using Module.MusicSourcesStorage.Gui.Entities.Abstract;
using Module.MusicSourcesStorage.Logic.Entities;

namespace Module.MusicSourcesStorage.Gui.Entities;

public sealed class AddingVkPostWithArchiveContext : IAddingVkPostWithArchiveContext
{
    public string? Error { get; set; }

    public string? SuccessResultText { get; set; }

    public VkPostGlobalId? PostId { get; set; }
    public IReadOnlyList<VkDocument>? AttachedDocuments { get; set; }
    public VkDocument? SelectedDocument { get; set; }
    public IReadOnlyList<MusicSourceFile>? IndexedFiles { get; set; }
}