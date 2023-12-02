using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;

namespace Module.MusicSourcesStorage.Gui.AbstractViewModels;

public interface IWizardVM
{
    IWizardStepVM CurrentStep { get; }

    bool HasNextStep { get; }
    string? CustomNextCommandName { get; }

    bool HasPreviousStep { get; }

    string? CustomCloseWizardCommandName { get; }

    ICommand Next { get; }
    ICommand Back { get; }
    ICommand Close { get; }
}