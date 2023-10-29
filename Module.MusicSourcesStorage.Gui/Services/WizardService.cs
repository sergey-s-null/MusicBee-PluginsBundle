using Autofac;
using Module.MusicSourcesStorage.Gui.Entities;
using Module.MusicSourcesStorage.Gui.Entities.Abstract;
using Module.MusicSourcesStorage.Gui.Enums;
using Module.MusicSourcesStorage.Gui.Services.Abstract;
using Module.MusicSourcesStorage.Gui.Views;

namespace Module.MusicSourcesStorage.Gui.Services;

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
                RegisterRootDescriptor(builder, wizardType);
                RegisterWizardContext(builder, wizardType);
            }
        );
    }

    private void RegisterRootDescriptor(ContainerBuilder builder, WizardType wizardType)
    {
        builder
            .RegisterInstance(_wizardPipelines.GetRootDescriptor(wizardType))
            .As<IWizardStepDescriptor>();
    }

    private static void RegisterWizardContext(ContainerBuilder builder, WizardType wizardType)
    {
        switch (wizardType)
        {
            case WizardType.AddingVkPostWithArchive:
                builder
                    .RegisterType<AddingVkPostWithArchiveContext>()
                    .As<IAddingVkPostWithArchiveContext>()
                    .As<IWizardErrorContext>()
                    .SingleInstance();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(wizardType), wizardType, "Wizard type is unknown.");
        }
    }
}