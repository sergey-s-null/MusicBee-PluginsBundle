using AutoMapper;
using Module.MusicSourcesStorage.Logic.Entities;
using VkNet.Model.Attachments;
using ImageFileModel = Module.MusicSourcesStorage.Database.Models.ImageFileModel;
using MusicFileModel = Module.MusicSourcesStorage.Database.Models.MusicFileModel;
using UnknownFileModel = Module.MusicSourcesStorage.Database.Models.UnknownFileModel;

namespace Module.MusicSourcesStorage.Logic;

public sealed class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Document, VkDocumentModel>()
            .ConstructUsing(x => new VkDocumentModel(
                x.Id!.Value,
                x.OwnerId!.Value,
                x.Title,
                x.Uri,
                x.Size
            ));

        CreateMap<VkDocumentModel, Database.Models.VkDocumentModel>()
            .ConstructUsing(x => new Database.Models.VkDocumentModel
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