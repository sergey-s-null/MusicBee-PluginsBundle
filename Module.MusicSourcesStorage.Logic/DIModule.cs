using Autofac;
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
            .RegisterType<VkDocumentDownloader>()
            .As<IVkDocumentDownloader>()
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
            .RegisterGeneric(typeof(HierarchyBuilder<,>))
            .As(typeof(IHierarchyBuilder<,>));
        builder
            .RegisterType<MusicSourcesStorageService>()
            .As<IMusicSourcesStorageService>()
            .SingleInstance();
    }
}