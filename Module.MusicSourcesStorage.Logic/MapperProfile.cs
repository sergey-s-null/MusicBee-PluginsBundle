﻿using AutoMapper;
using Module.MusicSourcesStorage.Database.Models;
using Module.MusicSourcesStorage.Logic.Entities;
using VkNet.Model.Attachments;

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

        CreateMap<VkDocumentModel, VkDocumentInfo>()
            .ConstructUsing(x => new VkDocumentInfo
            {
                Id = x.Id,
                OwnerId = x.OwnerId,
                Name = x.Name,
                Uri = x.Uri,
                Size = x.Size,
            });

        CreateMap<IndexedFile, MusicFile>()
            .ConstructUsing(x => new MusicFile
            {
                Path = x.Path,
                Size = x.Size,
                IsListened = false
            });
        CreateMap<IndexedFile, ImageFile>()
            .ConstructUsing(x => new ImageFile
            {
                Path = x.Path,
                Size = x.Size,
                IsCover = false,
                Data = null
            });
        CreateMap<IndexedFile, UnknownFile>()
            .ConstructUsing(x => new UnknownFile
            {
                Path = x.Path,
                Size = x.Size
            });
    }
}