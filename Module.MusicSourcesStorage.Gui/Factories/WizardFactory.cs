using Autofac;
using Module.MusicSourcesStorage.Gui.Enums;
using Module.MusicSourcesStorage.Gui.Factories.Abstract;
using Module.MusicSourcesStorage.Gui.Views;

namespace Module.MusicSourcesStorage.Gui.Factories;

public sealed class WizardFactory : IWizardFactory
{
    private readonly ILifetimeScope _lifetimeScope;

    public WizardFactory(ILifetimeScope lifetimeScope)
    {
        _lifetimeScope = lifetimeScope;
    }

    public Wizard Create(WizardType wizardType)
    {
        using var scope = _lifetimeScope.BeginLifetimeScope(wizardType);
        return scope.Resolve<Wizard>();
    }
}