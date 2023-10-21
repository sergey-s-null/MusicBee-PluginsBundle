using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Entities;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels.WizardSteps;

public sealed class SuccessResultStepDTVM : ISuccessResultStepVM
{
    public event EventHandler<StepTransitionEventArgs>? StepTransitionRequested;
    public event EventHandler? CloseWizardRequested;

    public bool HasNextStep => true;
    public bool CanGoNext => true;
    public string? CustomNextStepName => "Done";

    public bool HasPreviousStep => false;
    public bool CanGoBack => false;

    public ICommand Back => null!;
    public ICommand Next => null!;
    // todo make custom text for close wizard
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