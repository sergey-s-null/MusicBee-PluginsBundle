using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Entities;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels.WizardSteps;

public sealed class SuccessResultStepDTVM : ISuccessResultStepVM
{
    public event EventHandler<StepTransitionEventArgs>? StepTransitionRequested;
    public event EventHandler? CloseWizardRequested;

    public bool HasNextStep => false;
    public bool CanGoNext => false;
    public string? CustomNextStepName => null;

    public bool HasPreviousStep => false;
    public bool CanGoBack => false;

    public string? CustomCloseWizardCommandName => "Done";

    public ICommand Back => null!;
    public ICommand Next => null!;
    public ICommand CloseWizard => null!;

    public string Text { get; }

    public SuccessResultStepDTVM() : this("Very Big step for Humanity")
    {
    }

    public SuccessResultStepDTVM(string text)
    {
        Text = text;
    }
}