using System.Windows.Input;

namespace Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;

public interface IManualWizardStepVM : IWizardStepVM
{
    bool HasNextStep { get; }
    bool CanGoNext { get; }
    string? CustomNextStepName { get; }

    bool HasPreviousStep { get; }
    bool CanGoBack { get; }

    string? CustomCloseWizardCommandName { get; }

    ICommand Next { get; }
    ICommand Back { get; }
    ICommand CloseWizard { get; }
}