using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Entities;
using Module.MusicSourcesStorage.Gui.Enums;
using Module.MusicSourcesStorage.Gui.Factories.Abstract;

namespace Module.MusicSourcesStorage.Gui.Factories;

public sealed class WizardStepDescriptorFactory : IWizardStepDescriptorFactory
{
    private readonly Func<IWizardStepViewModelsFactory> _wizardStepViewModelsFactoryProvider;

    public WizardStepDescriptorFactory(Func<IWizardStepViewModelsFactory> wizardStepViewModelsFactoryProvider)
    {
        _wizardStepViewModelsFactoryProvider = wizardStepViewModelsFactoryProvider;
    }

    public WizardStepDescriptor Create(StepType stepType)
    {
        return new WizardStepDescriptor(stepType, _wizardStepViewModelsFactoryProvider());
    }

    public WizardStepDescriptor Create(Func<IWizardStepVM> stepVMFactory)
    {
        return new WizardStepDescriptor(stepVMFactory);
    }
}