using Autofac;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Entities.Abstract;
using Module.MusicSourcesStorage.Gui.Enums;
using Module.MusicSourcesStorage.Gui.Factories.Abstract;

namespace Module.MusicSourcesStorage.Gui.Factories;

public sealed class WizardStepViewModelsFactory : IWizardStepViewModelsFactory
{
    private readonly ILifetimeScope _lifetimeScope;

    private readonly IDictionary<StepType, Func<IWizardStepVM>> _factories;

    public WizardStepViewModelsFactory(ILifetimeScope lifetimeScope)
    {
        _lifetimeScope = lifetimeScope;

        _factories = new Dictionary<StepType, Func<IWizardStepVM>>();
    }

    public IWizardStepVM Create(IWizardStepDescriptor descriptor)
    {
        if (descriptor is ISuccessStepDescriptor successStepDescriptor)
        {
            return _lifetimeScope.ResolveKeyed<IWizardStepVM>(
                successStepDescriptor.StepType,
                new TypedParameter(typeof(string), successStepDescriptor.Text)
            );
        }

        if (_factories.TryGetValue(descriptor.StepType, out var factory))
        {
            return factory();
        }

        if (!_lifetimeScope.TryResolveKeyed(descriptor.StepType, out factory))
        {
            throw new ArgumentOutOfRangeException(
                nameof(descriptor.StepType),
                descriptor.StepType,
                "Could not resolve wizard step vm factory for specified step type."
            );
        }

        _factories[descriptor.StepType] = factory;
        return factory();
    }
}