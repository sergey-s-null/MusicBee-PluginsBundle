namespace Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;

public interface ISourceAddingWizardStepVM
{
    bool HasNextStep { get; }
    bool CanGoNext { get; }

    bool HasPreviousStep { get; }
    bool CanGoBack { get; }

    ISourceAddingWizardStepVM GoNext();

    ISourceAddingWizardStepVM GoBack();
}