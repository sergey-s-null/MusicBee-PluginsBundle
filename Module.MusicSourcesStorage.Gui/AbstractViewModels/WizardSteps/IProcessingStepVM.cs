using System.Windows.Input;

namespace Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;

public interface IProcessingStepVM : IAutomaticWizardStepVM
{
    string Text { get; }

    ICommand Cancel { get; }
}