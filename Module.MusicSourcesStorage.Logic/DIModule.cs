using Autofac;
using Module.MusicSourcesStorage.Logic.Factories;
using Module.MusicSourcesStorage.Logic.Factories.Abstract;
using Module.MusicSourcesStorage.Logic.Services;
using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Logic;

public sealed class DIModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterType<VkService>()
            .As<IVkService>()
            .SingleInstance();
        builder
            .RegisterType<CoverSelectionService>()
            .As<ICoverSelectionService>()
            .SingleInstance();
        builder
            .RegisterType<FilesDownloadingService>()
            .As<IFilesDownloadingService>()
            .SingleInstance();
        builder
            .RegisterType<VkArchiveFilesDownloadingService>()
            .As<IVkArchiveFilesDownloadingService>()
            .SingleInstance();
        builder
            .RegisterType<VkDocumentDownloadingTaskManager>()
            .As<IVkDocumentDownloadingTaskManager>()
            .SingleInstance();
        builder
            .RegisterType<DownloadedVkDocumentsCache>()
            .As<IDownloadedVkDocumentsCache>()
            .SingleInstance();
        builder
            .RegisterType<ArchiveIndexer>()
            .As<IArchiveIndexer>()
            .SingleInstance();
        builder
            .RegisterType<FileClassifier>()
            .As<IFileClassifier>()
            .SingleInstance();
        builder
            .RegisterType<HierarchyBuilderFactory>()
            .As<IHierarchyBuilderFactory>()
            .SingleInstance();
        builder
            .RegisterGeneric(typeof(HierarchyBuilder<,>))
            .As(typeof(IHierarchyBuilder<,>));
        builder
            .RegisterType<FilesLocatingService>()
            .As<IFilesLocatingService>()
            .SingleInstance();
        builder
            .RegisterType<FilesDeletingService>()
            .As<IFilesDeletingService>()
            .SingleInstance();
        builder
            .RegisterType<SourceFilesRetargetingService>()
            .As<ISourceFilesRetargetingService>()
            .SingleInstance();
        builder
            .RegisterType<MusicSourcesStorageService>()
            .As<IMusicSourcesStorageService>()
            .SingleInstance();
        builder
            .RegisterType<SourceFilesPathService>()
            .As<ISourceFilesPathService>()
            .SingleInstance();
        builder
            .RegisterType<VkDocumentDownloader>()
            .As<IVkDocumentDownloader>()
            .SingleInstance();
        builder
            .RegisterType<ArchiveExtractor>()
            .As<IArchiveExtractor>()
            .SingleInstance();
        builder
            .RegisterType<ImageService>()
            .As<IImageService>()
            .SingleInstance();
    }
}