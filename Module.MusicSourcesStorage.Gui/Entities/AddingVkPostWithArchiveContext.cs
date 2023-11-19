using Module.MusicSourcesStorage.Gui.Entities.Abstract;
using Module.MusicSourcesStorage.Logic.Entities;

namespace Module.MusicSourcesStorage.Gui.Entities;

public sealed class AddingVkPostWithArchiveContext :
    IAddingVkPostWithArchiveContext,
    IInitialMusicSourceAdditionalInfoContext
{
    public string? Error { get; set; }

    public VkOwnedEntityId? PostId { get; set; }
    public IReadOnlyList<VkDocument>? AttachedDocuments { get; set; }
    public VkDocument? SelectedDocument { get; set; }
    public IReadOnlyList<SourceFile>? IndexedFiles { get; set; }
    public MusicSourceAdditionalInfo? InitialAdditionalInfo { get; set; }
    public MusicSourceAdditionalInfo? EditedAdditionalInfo { get; set; }

    public MusicSource? Result { get; set; }
}