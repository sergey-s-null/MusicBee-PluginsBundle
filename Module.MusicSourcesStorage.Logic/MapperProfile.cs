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

        CreateMap<IndexedFile, MusicFileModel>()
            .ConstructUsing(x => new MusicFileModel
            {
                Path = x.Path,
                Size = x.Size,
                IsListened = false
            });
        CreateMap<IndexedFile, ImageFileModel>()
            .ConstructUsing(x => new ImageFileModel
            {
                Path = x.Path,
                Size = x.Size,
                IsCover = false,
                Data = null
            });
        CreateMap<IndexedFile, UnknownFileModel>()
            .ConstructUsing(x => new UnknownFileModel
            {
                Path = x.Path,
                Size = x.Size
            });
    }
}