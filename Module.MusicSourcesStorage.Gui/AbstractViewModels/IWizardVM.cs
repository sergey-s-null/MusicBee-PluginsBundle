using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;

namespace Module.MusicSourcesStorage.Gui.AbstractViewModels;

public interface IWizardVM
{
    IWizardStepVM CurrentStep { get; }
}