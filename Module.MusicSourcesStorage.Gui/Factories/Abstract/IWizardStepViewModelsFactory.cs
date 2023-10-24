using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Enums;

namespace Module.MusicSourcesStorage.Gui.Factories.Abstract;

public interface IWizardStepViewModelsFactory
{
    IWizardStepVM Create(StepType stepType);
}