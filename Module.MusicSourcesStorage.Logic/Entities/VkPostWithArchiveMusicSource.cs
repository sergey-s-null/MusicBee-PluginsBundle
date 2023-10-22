namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed record VkPostWithArchiveMusicSource(
    ulong PostOwnerId,
    ulong PostId
);