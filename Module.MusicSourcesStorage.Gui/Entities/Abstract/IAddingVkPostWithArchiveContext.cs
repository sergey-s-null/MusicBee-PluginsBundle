using Module.MusicSourcesStorage.Logic.Entities;

namespace Module.MusicSourcesStorage.Gui.Entities.Abstract;

public interface IAddingVkPostWithArchiveContext : IWizardErrorContext
{
    VkPostGlobalId? PostId { get; set; }
    IReadOnlyList<VkDocumentModel>? AttachedDocuments { get; set; }
    VkDocumentModel? SelectedDocument { get; set; }
    IReadOnlyList<IndexedFile>? IndexedFiles { get; set; }
}