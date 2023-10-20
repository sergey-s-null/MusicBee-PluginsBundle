using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Entities;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels.WizardSteps;

public sealed class ManualWizardStepDTVM : IManualWizardStepVM
{
    public event EventHandler<StepTransitionEventArgs>? NextStepRequested;
    public event EventHandler<StepTransitionEventArgs>? PreviousStepRequested;
    public event EventHandler? CloseWizardRequested;

    public bool HasNextStep => false;
    public bool CanGoNext => false;
    public string? CustomNextStepName => null;

    public bool HasPreviousStep => true;
    public bool CanGoBack => false;

    public ICommand Back => null!;
    public ICommand Next => null!;
    public ICommand CloseWizard => null!;
}