using System.Windows.Input;

namespace Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;

public interface IProcessingStepVM : IAutomaticWizardStepVM
{
    string Text { get; }
    IProgressVM? Progress { get; }

    ICommand Cancel { get; }
}