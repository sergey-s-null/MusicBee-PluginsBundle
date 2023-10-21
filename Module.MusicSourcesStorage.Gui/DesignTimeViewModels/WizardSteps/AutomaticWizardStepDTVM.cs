using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Entities;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels.WizardSteps;

public sealed class AutomaticWizardStepDTVM : IAutomaticWizardStepVM
{
    public event EventHandler<StepTransitionEventArgs>? StepTransitionRequested;
    public event EventHandler? CloseWizardRequested;

    public bool CanSafelyCloseWizard => false;

    public void Start()
    {
        throw new NotImplementedException();
    }
}