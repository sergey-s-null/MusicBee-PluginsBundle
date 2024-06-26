﻿using Autofac;
using Autofac.Features.AttributeFilters;
using Module.Core.Enums;
using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Commands;
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
        RegisterCommands(builder);
        RegisterServices(builder);
        RegisterFactories(builder);
    }

    private static void RegisterEntities(ContainerBuilder builder)
    {
        builder
            .RegisterType<WizardPipelines>()
            .As<IWizardPipelines>()
            .SingleInstance();
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
            .RegisterType<MusicSourcesStorageSettingsVM>()
            .As<IMusicSourcesStorageSettingsVM>();
        builder
            .RegisterType<MusicSourcesWindowVM>()
            .As<IMusicSourcesWindowVM>();
        builder
            .RegisterType<WizardVM>()
            .As<IWizardVM>();
        builder
            .RegisterType<MusicSourceVM>()
            .As<IMusicSourceVM>();
        builder
            .RegisterType<ConnectedDirectoryVM>()
            .AsSelf();
        builder
            .RegisterType<ConnectedMusicFileVM>()
            .As<IConnectedMusicFileVM>();
        builder
            .RegisterType<ConnectedImageFileVM>()
            .As<IConnectedImageFileVM>();
        builder
            .RegisterType<ConnectedUnknownFileVM>()
            .As<IConnectedUnknownFileVM>();
        builder
            .RegisterType<DirectoryVM>()
            .As<IDirectoryVM>();
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
            .RegisterType<SelectTorrentFileStepVM>()
            .Keyed<IWizardStepVM>(StepType.SelectTorrentFile);
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
            .RegisterType<IndexTorrentStepVM>()
            .Keyed<IWizardStepVM>(StepType.IndexTorrent);
        builder
            .RegisterType<IndexingResultStepVM>()
            .Keyed<IWizardStepVM>(StepType.IndexingResult);
        builder
            .RegisterType<EditMusicSourceAdditionalInfoStepVM>()
            .Keyed<IWizardStepVM>(StepType.EditMusicSourceAdditionalInfo);
        builder
            .RegisterType<AddVkPostWithArchiveSourceToDatabaseStepVM>()
            .Keyed<IWizardStepVM>(StepType.AddVkPostWithArchiveSourceToDatabase);
        builder
            .RegisterType<AddTorrentSourceToDatabaseStepVM>()
            .Keyed<IWizardStepVM>(StepType.AddTorrentSourceToDatabase);
        builder
            .RegisterType<ReceiveMusicSourceAdditionalInfoStepVM>()
            .Keyed<IWizardStepVM>(StepType.ReceiveMusicSourceAdditionalInfo);
        builder
            .RegisterType<UpdateMusicSourceAdditionalInfoInDatabaseVM>()
            .Keyed<IWizardStepVM>(StepType.UpdateMusicSourceAdditionalInfoInDatabase);
        builder
            .RegisterType<RetargetSourceFilesStepVM>()
            .Keyed<IWizardStepVM>(StepType.RetargetSourceFiles);
    }

    private static void RegisterCommands(ContainerBuilder builder)
    {
        builder
            .RegisterType<DownloadFileCommand>()
            .AsSelf();
        builder
            .RegisterType<DeleteFileCommand>()
            .AsSelf();
        builder
            .RegisterType<SelectAsCoverCommand>()
            .AsSelf();
        builder
            .RegisterType<RemoveCoverCommand>()
            .AsSelf();
        builder
            .RegisterType<ChangeMusicListenedStateCommand>()
            .AsSelf();
        builder
            .RegisterType<DeleteMusicFileAndMarkAsListenedCommand>()
            .AsSelf();
    }

    private static void RegisterServices(ContainerBuilder builder)
    {
        builder
            .RegisterType<WizardService>()
            .As<IWizardService>()
            .SingleInstance();
        builder
            .RegisterType<MusicSourceVMBuilder>()
            .As<IMusicSourceVMBuilder>()
            .InstancePerMatchingLifetimeScope(Scope.UiDispatcher);
        builder
            .RegisterType<ConnectedNodesHierarchyVMBuilder>()
            .WithAttributeFiltering()
            .As<IConnectedNodesHierarchyVMBuilder>()
            .InstancePerMatchingLifetimeScope(Scope.UiDispatcher);
        builder
            .RegisterType<NodesHierarchyVMBuilder>()
            .WithAttributeFiltering()
            .As<INodesHierarchyVMBuilder>()
            .SingleInstance();
        builder
            .RegisterType<ConnectedFileVMBuilder>()
            .As<IConnectedFileVMBuilder>()
            .InstancePerMatchingLifetimeScope(Scope.UiDispatcher);
        builder
            .RegisterType<FileVMBuilder>()
            .As<IFileVMBuilder>()
            .SingleInstance();
        builder
            .RegisterType<FileOperationLocker>()
            .As<IFileOperationLocker>()
            .SingleInstance();
    }

    private static void RegisterFactories(ContainerBuilder builder)
    {
        builder
            .RegisterType<WizardStepVMFactory>()
            .As<IWizardStepVMFactory>();
    }
}