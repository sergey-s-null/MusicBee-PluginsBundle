using Module.MusicSourcesStorage.Gui.Enums;

namespace Module.MusicSourcesStorage.Gui.Entities.Abstract;

public interface IWizardStepDescriptor
{
    StepType StepType { get; }

    IWizardStepDescriptor? NextStepDescriptor { get; }
    IWizardStepDescriptor? PreviousStepDescriptor { get; }
    IWizardStepDescriptor? ErrorStepDescriptor { get; }

    string? CustomNextCommandName { get; }
    string? CustomCloseWizardCommandName { get; }
    bool CanSafelyCloseWizard { get; }
}