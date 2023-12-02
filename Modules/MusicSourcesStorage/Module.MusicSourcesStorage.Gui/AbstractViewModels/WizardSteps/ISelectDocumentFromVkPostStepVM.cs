namespace Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;

public interface ISelectDocumentFromVkPostStepVM : IManualWizardStepVM
{
    long PostOwnerId { get; }
    long PostId { get; }

    IReadOnlyList<IVkDocumentVM> Documents { get; }
    IVkDocumentVM? SelectedDocument { get; set; }
}