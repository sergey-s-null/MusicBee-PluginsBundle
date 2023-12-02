using Module.MusicSourcesStorage.Gui.Entities.Abstract;
using Module.MusicSourcesStorage.Gui.Enums;

namespace Module.MusicSourcesStorage.Gui.Entities;

public class WizardStepDescriptor : IWizardStepDescriptor
{
    public StepType StepType { get; }

    public IWizardStepDescriptor? NextStepDescriptor { get; set; }
    public IWizardStepDescriptor? PreviousStepDescriptor { get; set; }
    public IWizardStepDescriptor? ErrorStepDescriptor { get; set; }

    public string? CustomNextCommandName { get; set; }
    public string? CustomCloseWizardCommandName { get; set; }
    public bool CanSafelyCloseWizard { get; set; }

    public WizardStepDescriptor(StepType stepType)
    {
        StepType = stepType;
    }
}