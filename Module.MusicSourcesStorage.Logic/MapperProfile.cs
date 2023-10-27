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

        CreateMap<VkDocument, VkDocumentInfo>()
            .ConstructUsing(x => new VkDocumentInfo
            {
                Id = x.Id,
                OwnerId = x.OwnerId,
                Name = x.Name,
                Uri = x.Uri,
                Size = x.Size,
            });

        CreateMap<MusicSourceFile, MusicFile>()
            .ConstructUsing(x => new MusicFile
            {
                Path = x.Path,
                Size = x.Size,
                IsListened = false
            });
        CreateMap<MusicSourceFile, ImageFile>()
            .ConstructUsing(x => new ImageFile
            {
                Path = x.Path,
                Size = x.Size,
                IsCover = false,
                Data = null
            });
        CreateMap<MusicSourceFile, UnknownFile>()
            .ConstructUsing(x => new UnknownFile
            {
                Path = x.Path,
                Size = x.Size
            });
    }
}