using Autofac;
using Module.MusicSourcesStorage.Gui.Entities;
using Module.MusicSourcesStorage.Gui.Entities.Abstract;
using Module.MusicSourcesStorage.Gui.Enums;
using Module.MusicSourcesStorage.Gui.Factories.Abstract;
using Module.MusicSourcesStorage.Gui.Views;

namespace Module.MusicSourcesStorage.Gui.Factories;

public sealed class WizardService : IWizardService
{
    private readonly ILifetimeScope _lifetimeScope;
    private readonly IWizardPipelines _wizardPipelines;

    public WizardService(
        ILifetimeScope lifetimeScope,
        IWizardPipelines wizardPipelines)
    {
        _wizardPipelines = wizardPipelines;
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
            builder =>
            {
                builder
                    .RegisterInstance(_wizardPipelines.GetRootDescriptor(wizardType))
                    .As<IWizardStepDescriptor>();

                // todo use difference context for diff wizards
                builder
                    .RegisterType<AddingVkPostWithArchiveContext>()
                    .As<IAddingVkPostWithArchiveContext>()
                    .As<IWizardErrorContext>()
                    .SingleInstance();
            }
        );
    }
}