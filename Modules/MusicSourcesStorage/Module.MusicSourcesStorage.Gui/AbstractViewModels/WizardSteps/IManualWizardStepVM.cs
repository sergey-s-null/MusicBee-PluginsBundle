using Module.MusicSourcesStorage.Gui.Enums;

namespace Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;

public interface IManualWizardStepVM : IWizardStepVM
{
    bool IsValidState { get; }

    StepResult Confirm();
}