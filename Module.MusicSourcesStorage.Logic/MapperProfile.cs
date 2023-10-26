using AutoMapper;
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
    }
}