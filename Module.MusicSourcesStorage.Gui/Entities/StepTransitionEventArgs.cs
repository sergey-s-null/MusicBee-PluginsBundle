using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;

namespace Module.MusicSourcesStorage.Gui.Entities;

public sealed class StepTransitionEventArgs : EventArgs
{
    public IWizardStepVM Step { get; }

    public StepTransitionEventArgs(IWizardStepVM step)
    {
        Step = step;
    }
}