namespace Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;

public interface ISelectVkPostAttachmentStepVM : IManualWizardStepVM
{
    ulong PostOwnerId { get; }
    ulong PostId { get; }

    IReadOnlyList<IVkPostAttachmentVM> Attachments { get; }
}