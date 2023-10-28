using Autofac;
using Autofac.Core;
using Autofac.Features.AttributeFilters;
using AutoMapper;
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
using Module.MusicSourcesStorage.Gui.ViewModels.Nodes;
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
            .RegisterType<AddingVkPostWithArchiveContext>()
            .As<IAddingVkPostWithArchiveContext>()
            .As<IWizardErrorContext>()
            .InstancePerMatchingLifetimeScope(WizardType.AddingVkPostWithArchive);
    }

    private static void RegisterViews(ContainerBuilder builder)
    {
        builder
            .RegisterType<MusicSourcesWindow>()
            .AsSelf();
        builder
            .RegisterType<Wizard>()
            .AsSelf();
    }

    private static void RegisterViewModels(ContainerBuilder builder)
    {
        builder
            .RegisterType<MusicSourcesWindowVM>()
            .WithAttributeFiltering()
            .As<IMusicSourcesWindowVM>();
        builder
            .RegisterType<WizardVM>()
            .As<IWizardVM>();
    }

    private static void RegisterStepViewModels(ContainerBuilder builder)
    {
        builder
            .RegisterType<SuccessResultStepVM>()
            .As<ISuccessResultStepVM>()
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
        builder
            .RegisterType<WizardService>()
            .As<IWizardService>()
            .SingleInstance();
        RegisterMusicSourceVMBuilder(builder, ConnectionState.Connected);
        RegisterMusicSourceVMBuilder(builder, ConnectionState.NotConnected);
        RegisterNodesHierarchyVMBuilder(builder, ConnectionState.Connected);
        RegisterNodesHierarchyVMBuilder(builder, ConnectionState.NotConnected);
    }

    private static void RegisterFactories(ContainerBuilder builder)
    {
        builder
            .RegisterType<WizardStepDescriptorFactory>()
            .As<IWizardStepDescriptorFactory>();
        builder
            .RegisterType<WizardStepViewModelsFactory>()
            .As<IWizardStepViewModelsFactory>();
        builder
            .Register<DirectoryVMFactory>(_ =>
                (name, nodes) => new DirectoryVM(name, nodes)
            )
            .Keyed<DirectoryVMFactory>(ConnectionState.NotConnected)
            .SingleInstance();
        builder
            .Register<DirectoryVMFactory>(_ =>
                (name, nodes) => new ConnectedDirectoryVM(name, nodes)
            )
            .Keyed<DirectoryVMFactory>(ConnectionState.Connected)
            .SingleInstance();
    }

    private static void RegisterMusicSourceVMBuilder(ContainerBuilder builder, ConnectionState connectionState)
    {
        builder
            .RegisterType<MusicSourceVMBuilder>()
            .WithParameter(ResolvedParameter.ForKeyed<INodesHierarchyVMBuilder>(connectionState))
            .Keyed<IMusicSourceVMBuilder>(connectionState)
            .SingleInstance();
    }

    private static void RegisterNodesHierarchyVMBuilder(ContainerBuilder builder, ConnectionState connectionState)
    {
        builder
            .RegisterType<NodesHierarchyVMBuilder>()
            .WithParameter(ResolvedParameter.ForKeyed<IMapper>(connectionState))
            .WithParameter(ResolvedParameter.ForKeyed<DirectoryVMFactory>(connectionState))
            .Keyed<INodesHierarchyVMBuilder>(connectionState)
            .SingleInstance();
    }
}