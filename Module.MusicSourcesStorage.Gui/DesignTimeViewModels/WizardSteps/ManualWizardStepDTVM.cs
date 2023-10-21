using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Entities;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels.WizardSteps;

public sealed class ManualWizardStepDTVM : IManualWizardStepVM
{
    public event EventHandler<StepTransitionEventArgs>? StepTransitionRequested;
    public event EventHandler? CloseWizardRequested;

    public bool HasNextStep => true;
    public bool CanGoNext => true;
    public string? CustomNextStepName => "Add";

    public bool HasPreviousStep => false;
    public bool CanGoBack => false;

    public ICommand Back => null!;
    public ICommand Next => null!;
    public ICommand CloseWizard => null!;
}