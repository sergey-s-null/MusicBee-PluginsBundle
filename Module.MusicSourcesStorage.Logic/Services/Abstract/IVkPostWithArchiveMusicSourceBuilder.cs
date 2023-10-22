using Module.MusicSourcesStorage.Logic.Entities;

namespace Module.MusicSourcesStorage.Logic.Services.Abstract;

public interface IVkPostWithArchiveMusicSourceBuilder
{
    ulong? PostOwnerId { get; set; }
    ulong? PostId { get; set; }

    VkPostWithArchiveMusicSource Build();
}