using AutoMapper;
using Module.MusicSourcesStorage.Database.Models;
using Module.MusicSourcesStorage.Logic.Entities;
using VkNet.Model.Attachments;

namespace Module.MusicSourcesStorage.Logic;

public sealed class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Document, VkDocument>()
            .ConstructUsing(x => new VkDocument(
                x.Id!.Value,
                x.OwnerId!.Value,
                x.Title,
                x.Uri,
                x.Size
            ));

        CreateMap<VkDocument, VkDocumentModel>()
            .ConstructUsing(x => new VkDocumentModel
            {
                Id = x.Id,
                OwnerId = x.OwnerId,
                Name = x.Name,
                Uri = x.Uri,
                Size = x.Size,
            });

        CreateMap<MusicFile, MusicFileModel>()
            .IncludeBase<SourceFile, FileModel>()
            .ConstructUsing(x => new MusicFileModel
            {
                Id = x.Id,
                Path = x.Path,
                Size = x.Size,
                IsListened = false
            });
        CreateMap<ImageFile, ImageFileModel>()
            .IncludeBase<SourceFile, FileModel>()
            .ConstructUsing(x => new ImageFileModel
            {
                Id = x.Id,
                Path = x.Path,
                Size = x.Size,
                IsCover = false,
                Data = null
            });
        CreateMap<UnknownFile, UnknownFileModel>()
            .IncludeBase<SourceFile, FileModel>()
            .ConstructUsing(x => new UnknownFileModel
            {
                Id = x.Id,
                Path = x.Path,
                Size = x.Size
            });
    }
}