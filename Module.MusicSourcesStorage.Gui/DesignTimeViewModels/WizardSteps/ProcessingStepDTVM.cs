using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Entities;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels.WizardSteps;

public sealed class ProcessingStepDTVM : IProcessingStepVM
{
    public event EventHandler<StepTransitionEventArgs>? NextStepRequested;
    public event EventHandler<StepTransitionEventArgs>? PreviousStepRequested;
    public event EventHandler? CloseWizardRequested;

    public string Text => "Do very very very very hard work";

    public ICommand Cancel => null!;

    public void Start()
    {
        throw new NotImplementedException();
    }
}