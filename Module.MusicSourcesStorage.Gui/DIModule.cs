using Autofac;
using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Enums;
using Module.MusicSourcesStorage.Gui.Factories;
using Module.MusicSourcesStorage.Gui.Factories.Abstract;
using Module.MusicSourcesStorage.Gui.ViewModels;
using Module.MusicSourcesStorage.Gui.ViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Views;

namespace Module.MusicSourcesStorage.Gui;

public sealed class DIModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        RegisterViews(builder);
        RegisterViewModels(builder);
        RegisterStepViewModels(builder);
        RegisterFactories(builder);
    }

    private static void RegisterViews(ContainerBuilder builder)
    {
        builder
            .RegisterType<Wizard>()
            .AsSelf();
    }

    private static void RegisterViewModels(ContainerBuilder builder)
    {
        builder
            .RegisterType<WizardVM>()
            .As<IWizardVM>();
    }

    private static void RegisterStepViewModels(ContainerBuilder builder)
    {
        builder
            .RegisterType<SuccessResultStepVM>()
            .Keyed<IWizardStepVM>(StepType.SuccessResult);
        builder
            .RegisterType<ErrorStepVM>()
            .Keyed<IWizardStepVM>(StepType.Error);
        builder
            .RegisterType<SelectVkPostStepVM>()
            .Keyed<IWizardStepVM>(StepType.SelectVkPost);
        builder
            .RegisterType<ReceiveVkPostDocumentsStepVM>()
            .Keyed<IWizardStepVM>(StepType.ReceiveVkPostDocumentsStepVM);
        builder
            .RegisterType<SelectDocumentFromVkPostStepVM>()
            .Keyed<IWizardStepVM>(StepType.SelectDocumentFromVkPost);
        builder
            .RegisterType<DownloadAndIndexArchiveStepVM>()
            .Keyed<IWizardStepVM>(StepType.DownloadAndIndexArchive);
        builder
            .RegisterType<IndexingResultStepVM>()
            .Keyed<IWizardStepVM>(StepType.IndexingResult);
        builder
            .RegisterType<AddMusicSourceToDatabaseStepVM>()
            .Keyed<IWizardStepVM>(StepType.AddMusicSourceToDatabase);
    }

    private static void RegisterFactories(ContainerBuilder builder)
    {
        builder
            .RegisterType<WizardService>()
            .As<IWizardService>()
            .SingleInstance();
        builder
            .RegisterType<WizardStepViewModelsFactory>()
            .As<IWizardStepViewModelsFactory>()
            .SingleInstance();
    }
}