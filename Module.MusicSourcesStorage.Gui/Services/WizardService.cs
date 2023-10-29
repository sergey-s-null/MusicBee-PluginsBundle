using Autofac;
using Module.MusicSourcesStorage.Gui.Entities;
using Module.MusicSourcesStorage.Gui.Entities.Abstract;
using Module.MusicSourcesStorage.Gui.Enums;
using Module.MusicSourcesStorage.Gui.Services.Abstract;
using Module.MusicSourcesStorage.Gui.Views;
using Module.MusicSourcesStorage.Logic.Entities;

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

    public MusicSource? AddVkPostWithArchiveSource()
    {
        var context = new AddingVkPostWithArchiveContext();

        var wizardScope = _lifetimeScope.BeginLifetimeScope(builder =>
        {
            RegisterRootDescriptor(builder, WizardType.AddingVkPostWithArchive);
            builder
                .RegisterInstance(context)
                .As<IAddingVkPostWithArchiveContext>()
                .As<IMusicSourceAdditionalInfoContext>()
                .As<IWizardErrorContext>()
                .As<IWizardResultContext<MusicSource>>()
                .SingleInstance();
        });

        var wizard = wizardScope.Resolve<Wizard>();
        wizard.ShowDialog();

        return context.Result;
    }

    public void EditMusicSourceAdditionalInfo(int musicSourceId)
    {
        var context = new EditMusicSourceAdditionalInfoContext(musicSourceId);

        var wizardScope = _lifetimeScope.BeginLifetimeScope(builder =>
        {
            RegisterRootDescriptor(builder, WizardType.EditMusicSourceAdditionalInfo);
            builder
                .RegisterInstance(context)
                .As<IMusicSourceContext>()
                .As<IMusicSourceAdditionalInfoContext>()
                .As<IWizardErrorContext>();
        });

        var wizard = wizardScope.Resolve<Wizard>();
        wizard.ShowDialog();
    }

    private void RegisterRootDescriptor(ContainerBuilder builder, WizardType wizardType)
    {
        builder
            .RegisterInstance(_wizardPipelines.GetRootDescriptor(wizardType))
            .As<IWizardStepDescriptor>();
    }
}