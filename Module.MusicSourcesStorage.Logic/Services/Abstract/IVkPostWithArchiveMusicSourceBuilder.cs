using Module.MusicSourcesStorage.Logic.Entities;

namespace Module.MusicSourcesStorage.Logic.Services.Abstract;

public interface IVkPostWithArchiveMusicSourceBuilder
{
    VkPostGlobalId? PostId { get; set; }

    VkDocument? Document { get; set; }

    VkPostWithArchiveMusicSource Build();
}