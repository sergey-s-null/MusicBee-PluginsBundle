namespace Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;

public interface IErrorStepVM : IManualWizardStepVM
{
    string Error { get; }
}