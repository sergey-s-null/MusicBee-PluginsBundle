using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Entities.Abstract;

namespace Module.MusicSourcesStorage.Gui.Factories.Abstract;

public interface IWizardStepVMFactory
{
    IWizardStepVM Create(IWizardStepDescriptor descriptor);
}