using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;

namespace Module.MusicSourcesStorage.Gui.Entities.Abstract;

public interface IWizardStepDescriptor
{
    IWizardStepDescriptor? NextStepDescriptor { get; }
    IWizardStepDescriptor? PreviousStepDescriptor { get; }
    IWizardStepDescriptor? ErrorStepDescriptor { get; }

    string? CustomNextCommandName { get; }
    string? CustomCloseWizardCommandName { get; }
    bool CanSafelyCloseWizard { get; }

    IWizardStepVM CreateStepViewModel();
}