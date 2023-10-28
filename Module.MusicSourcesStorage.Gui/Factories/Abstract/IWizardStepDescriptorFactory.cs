using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Entities;
using Module.MusicSourcesStorage.Gui.Enums;

namespace Module.MusicSourcesStorage.Gui.Factories.Abstract;

public interface IWizardStepDescriptorFactory
{
    WizardStepDescriptor Create(StepType stepType);

    WizardStepDescriptor Create(Func<IWizardStepVM> stepVMFactory);
}