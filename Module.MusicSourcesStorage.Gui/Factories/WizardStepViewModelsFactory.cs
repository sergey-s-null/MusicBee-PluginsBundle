using Autofac;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
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

    public IWizardStepVM Create(StepType stepType)
    {
        if (_factories.TryGetValue(stepType, out var factory))
        {
            return factory();
        }

        if (!_lifetimeScope.TryResolveKeyed(stepType, out factory))
        {
            throw new ArgumentOutOfRangeException(
                nameof(stepType),
                stepType,
                "Could not resolve wizard step vm factory for specified step type."
            );
        }

        _factories[stepType] = factory;
        return factory();
    }
}