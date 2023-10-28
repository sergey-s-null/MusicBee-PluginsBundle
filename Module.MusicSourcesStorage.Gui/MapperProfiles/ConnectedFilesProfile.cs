using Autofac;
using AutoMapper;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using Module.MusicSourcesStorage.Gui.Factories;
using Module.MusicSourcesStorage.Logic.Entities;

namespace Module.MusicSourcesStorage.Gui.MapperProfiles;

public sealed class ConnectedFilesProfile : Profile
{
    public ConnectedFilesProfile(ILifetimeScope lifetimeScope)
    {
        var connectedMusicFileVMFactory = lifetimeScope.Resolve<ConnectedMusicFileVMFactory>();
        var connectedImageFileVMFactory = lifetimeScope.Resolve<ConnectedImageFileVMFactory>();
        var connectedUnknownFileVMFactory = lifetimeScope.Resolve<ConnectedUnknownFileVMFactory>();

        CreateMap<SourceFile, IFileVM>()
            .Include<MusicFile, IConnectedMusicFileVM>()
            .Include<ImageFile, IConnectedImageFileVM>()
            .Include<UnknownFile, IConnectedUnknownFileVM>();

        CreateMap<MusicFile, IConnectedMusicFileVM>()
            .ConstructUsing(x => connectedMusicFileVMFactory(x.Path));
        CreateMap<ImageFile, IConnectedImageFileVM>()
            .ConstructUsing(x => connectedImageFileVMFactory(x.Path));
        CreateMap<UnknownFile, IConnectedUnknownFileVM>()
            .ConstructUsing(x => connectedUnknownFileVMFactory(x.Path));
    }
}