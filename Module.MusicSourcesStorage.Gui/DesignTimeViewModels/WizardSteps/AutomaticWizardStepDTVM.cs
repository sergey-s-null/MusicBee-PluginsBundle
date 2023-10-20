using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Entities;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels.WizardSteps;

public sealed class AutomaticWizardStepDTVM : IAutomaticWizardStepVM
{
    public event EventHandler<StepTransitionEventArgs>? NextStepRequested;
    public event EventHandler<StepTransitionEventArgs>? PreviousStepRequested;
    public event EventHandler? CloseWizardRequested;

    public void Start()
    {
        throw new NotImplementedException();
    }
}