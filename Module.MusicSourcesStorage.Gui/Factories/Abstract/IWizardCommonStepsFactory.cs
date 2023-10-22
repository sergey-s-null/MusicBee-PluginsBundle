using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;

namespace Module.MusicSourcesStorage.Gui.Factories.Abstract;

public interface IWizardCommonStepsFactory
{
    IWizardStepVM CreateErrorStep(Exception e);

    IWizardStepVM CreateErrorStep(string error);
}