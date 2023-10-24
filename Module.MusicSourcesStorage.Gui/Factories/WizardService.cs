using Autofac;
using Module.MusicSourcesStorage.Gui.Entities.Abstract;
using Module.MusicSourcesStorage.Gui.Enums;
using Module.MusicSourcesStorage.Gui.Factories.Abstract;
using Module.MusicSourcesStorage.Gui.Views;

namespace Module.MusicSourcesStorage.Gui.Factories;

public sealed class WizardService : IWizardService
{
    private readonly ILifetimeScope _lifetimeScope;

    public WizardService(ILifetimeScope lifetimeScope)
    {
        _lifetimeScope = lifetimeScope;
    }

    public void Open(WizardType wizardType)
    {
        var wizardScope = CreateScope(wizardType);
        var wizard = wizardScope.Resolve<Wizard>();
        wizard.ShowDialog();
    }

    private ILifetimeScope CreateScope(WizardType wizardType)
    {
        return _lifetimeScope.BeginLifetimeScope(
            wizardType,
            builder => builder
                .Register(context =>
                {
                    var wizardPipelines = context.Resolve<IWizardPipelines>();
                    return wizardPipelines.GetRootDescriptor(wizardType);
                })
                .As<IWizardStepDescriptor>()
                .SingleInstance()
        );
    }
}