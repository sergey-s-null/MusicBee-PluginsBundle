using Module.MusicSourcesStorage.Gui.Entities.Abstract;
using Module.MusicSourcesStorage.Logic.Entities;

namespace Module.MusicSourcesStorage.Gui.Entities;

public sealed class AddingVkPostWithArchiveContext : IAddingVkPostWithArchiveContext
{
    public string? Error { get; set; }

    public VkPostGlobalId? PostId { get; set; }
    public IReadOnlyList<VkDocument>? AttachedDocuments { get; set; }
    public VkDocument? SelectedDocument { get; set; }
    public IReadOnlyList<SourceFile>? IndexedFiles { get; set; }
    public MusicSourceAdditionalInfo? AdditionalInfo { get; set; }

    public MusicSource? Result { get; set; }
}