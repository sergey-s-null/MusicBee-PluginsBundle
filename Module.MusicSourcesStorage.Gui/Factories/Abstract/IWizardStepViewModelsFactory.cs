using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Entities.Abstract;

namespace Module.MusicSourcesStorage.Gui.Factories.Abstract;

// todo rename to IWizardStepVMFactory
public interface IWizardStepViewModelsFactory
{
    IWizardStepVM Create(IWizardStepDescriptor descriptor);
}