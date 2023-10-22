namespace Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;

public interface ISelectDocumentFromVkPostStepVM : IManualWizardStepVM
{
    ulong PostOwnerId { get; }
    ulong PostId { get; }

    IReadOnlyList<IVkDocumentVM> Documents { get; }
    IVkDocumentVM? SelectedDocument { get; set; }
}