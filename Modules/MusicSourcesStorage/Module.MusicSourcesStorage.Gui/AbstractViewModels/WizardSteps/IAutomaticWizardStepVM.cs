using Module.MusicSourcesStorage.Gui.Entities;

namespace Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;

public interface IAutomaticWizardStepVM : IWizardStepVM
{
    event EventHandler<StepResultEventArgs> ProcessingCompleted;

    void Start();
}