using Module.MusicSourcesStorage.Logic.Entities;

namespace Module.MusicSourcesStorage.Logic.Services.Abstract;

public interface IVkPostWithArchiveMusicSourceBuilder
{
    VkPostGlobalId? PostId { get; set; }

    VkPostWithArchiveMusicSource Build();
}