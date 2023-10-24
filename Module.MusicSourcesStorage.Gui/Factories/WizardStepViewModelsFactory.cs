using Autofac;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Enums;
using Module.MusicSourcesStorage.Gui.Factories.Abstract;

namespace Module.MusicSourcesStorage.Gui.Factories;

public sealed class WizardStepViewModelsFactory : IWizardStepViewModelsFactory
{
    private readonly IReadOnlyDictionary<StepType, Func<IWizardStepVM>> _factories;

    public WizardStepViewModelsFactory(ILifetimeScope lifetimeScope)
    {
        // todo make lazy
        _factories = CreateFactories(lifetimeScope);
    }

    public IWizardStepVM Create(StepType stepType)
    {
        if (!_factories.TryGetValue(stepType, out var factory))
        {
            throw new ArgumentOutOfRangeException(nameof(stepType), stepType, "Unknown step type.");
        }

        return factory();
    }

    private IReadOnlyDictionary<StepType, Func<IWizardStepVM>> CreateFactories(ILifetimeScope lifetimeScope)
    {
        var stepTypes = Enum.GetValues(typeof(StepType)).OfType<StepType>();
        var factories = new Dictionary<StepType, Func<IWizardStepVM>>();
        foreach (var stepType in stepTypes)
        {
            var factory = lifetimeScope.ResolveKeyed<Func<IWizardStepVM>>(stepType);
            factories[stepType] = factory;
        }

        return factories;
    }
}