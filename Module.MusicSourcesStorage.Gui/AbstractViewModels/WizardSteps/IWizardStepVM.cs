using Module.MusicSourcesStorage.Gui.Entities;

namespace Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;

public interface IWizardStepVM
{
    event EventHandler<StepTransitionEventArgs> StepTransitionRequested;

    event EventHandler CloseWizardRequested;
}