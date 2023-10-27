using Autofac;
using Autofac.Core;
using Autofac.Features.AttributeFilters;
using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Entities;
using Module.MusicSourcesStorage.Gui.Entities.Abstract;
using Module.MusicSourcesStorage.Gui.Enums;
using Module.MusicSourcesStorage.Gui.Factories;
using Module.MusicSourcesStorage.Gui.Factories.Abstract;
using Module.MusicSourcesStorage.Gui.Services;
using Module.MusicSourcesStorage.Gui.Services.Abstract;
using Module.MusicSourcesStorage.Gui.ViewModels;
using Module.MusicSourcesStorage.Gui.ViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Views;

namespace Module.MusicSourcesStorage.Gui;

public sealed class DIModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        RegisterEntities(builder);
        RegisterViews(builder);
        RegisterViewModels(builder);
        RegisterStepViewModels(builder);
        RegisterServices(builder);
        RegisterFactories(builder);
    }

    private static void RegisterEntities(ContainerBuilder builder)
    {
        builder
            .RegisterType<WizardPipelines>()
            .As<IWizardPipelines>();
        builder
            .RegisterType<WizardStepDescriptor>()
            .AsSelf();
        builder
            .RegisterType<AddingVkPostWithArchiveContext>()
            .As<IAddingVkPostWithArchiveContext>()
            .As<IWizardErrorContext>()
            .InstancePerMatchingLifetimeScope(WizardType.AddingVkPostWithArchive);
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
            .Keyed<IWizardStepVM>(StepType.IndexingResult)
            .WithAttributeFiltering();
        builder
            .RegisterType<AddMusicSourceToDatabaseStepVM>()
            .Keyed<IWizardStepVM>(StepType.AddMusicSourceToDatabase);
    }

    private static void RegisterServices(ContainerBuilder builder)
    {
        RegisterNodesHierarchyVMBuilder(builder, ConnectionState.Connected);
        RegisterNodesHierarchyVMBuilder(builder, ConnectionState.NotConnected);
        RegisterNodeVMBuilder(builder, ConnectionState.Connected);
        RegisterNodeVMBuilder(builder, ConnectionState.NotConnected);
    }

    private static void RegisterFactories(ContainerBuilder builder)
    {
        builder
            .RegisterType<WizardService>()
            .As<IWizardService>()
            .SingleInstance();
        builder
            .RegisterType<WizardStepViewModelsFactory>()
            .As<IWizardStepViewModelsFactory>();
        builder
            .RegisterType<ConnectedNodeVMFactory>()
            .Keyed<INodeVMFactory>(ConnectionState.Connected)
            .SingleInstance();
        builder
            .RegisterType<NotConnectedNodeVMFactory>()
            .Keyed<INodeVMFactory>(ConnectionState.NotConnected)
            .SingleInstance();
    }

    private static void RegisterNodesHierarchyVMBuilder(ContainerBuilder builder, ConnectionState connectionState)
    {
        builder
            .RegisterType<NodesHierarchyVMBuilder>()
            .WithParameter(ResolvedParameter.ForKeyed<INodeVMFactory>(connectionState))
            .WithParameter(ResolvedParameter.ForKeyed<INodeVMBuilder>(connectionState))
            .Keyed<INodesHierarchyVMBuilder>(connectionState)
            .SingleInstance();
    }

    private static void RegisterNodeVMBuilder(ContainerBuilder builder, ConnectionState connectionState)
    {
        builder
            .RegisterType<NodeVMBuilder>()
            .WithParameter(ResolvedParameter.ForKeyed<INodeVMFactory>(connectionState))
            .Keyed<INodeVMBuilder>(connectionState)
            .SingleInstance();
    }
}