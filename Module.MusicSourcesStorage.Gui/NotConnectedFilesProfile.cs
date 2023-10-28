using AutoMapper;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using Module.MusicSourcesStorage.Gui.ViewModels.Nodes;
using Module.MusicSourcesStorage.Logic.Entities;

namespace Module.MusicSourcesStorage.Gui;

public sealed class NotConnectedFilesProfile : Profile
{
    public NotConnectedFilesProfile()
    {
        CreateMap<SourceFile, IFileVM>()
            .Include<MusicFile, IMusicFileVM>()
            .Include<ImageFile, IImageFileVM>()
            .Include<UnknownFile, IUnknownFileVM>();

        CreateMap<MusicFile, IMusicFileVM>()
            .ConstructUsing(x => new MusicFileVM(x.Path));
        CreateMap<ImageFile, IImageFileVM>()
            .ConstructUsing(x => new ImageFileVM(x.Path));
        CreateMap<UnknownFile, IUnknownFileVM>()
            .ConstructUsing(x => new UnknownFileVM(x.Path));
    }
}