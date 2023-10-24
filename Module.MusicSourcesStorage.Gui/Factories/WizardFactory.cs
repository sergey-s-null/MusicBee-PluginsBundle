using Autofac;
using Module.MusicSourcesStorage.Gui.Enums;
using Module.MusicSourcesStorage.Gui.Factories.Abstract;
using Module.MusicSourcesStorage.Gui.Views;

namespace Module.MusicSourcesStorage.Gui.Factories;

public sealed class WizardFactory : IWizardFactory
{
    private readonly IContainer _container;

    public WizardFactory(IContainer container)
    {
        _container = container;
    }

    public Wizard Create(WizardType wizardType)
    {
        using var scope = _container.BeginLifetimeScope(wizardType);
        return scope.Resolve<Wizard>();
    }
}