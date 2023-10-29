using Module.MusicSourcesStorage.Logic.Entities;

namespace Module.MusicSourcesStorage.Gui.Entities.Abstract;

public interface IAddingVkPostWithArchiveContext :
    IMusicSourceAdditionalInfoContext,
    IWizardErrorContext,
    IWizardResultContext<MusicSource>
{
    VkOwnedEntityId? PostId { get; set; }
    IReadOnlyList<VkDocument>? AttachedDocuments { get; set; }
    VkDocument? SelectedDocument { get; set; }
    IReadOnlyList<SourceFile>? IndexedFiles { get; set; }
}