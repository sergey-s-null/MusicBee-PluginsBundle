using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;

namespace Module.MusicSourcesStorage.Gui.AbstractViewModels;

public interface ISourceAddingWizardVM
{
    ISourceAddingWizardStepVM CurrentStep { get; }

    ICommand Back { get; }
    ICommand Next { get; }
    ICommand Cancel { get; }
}