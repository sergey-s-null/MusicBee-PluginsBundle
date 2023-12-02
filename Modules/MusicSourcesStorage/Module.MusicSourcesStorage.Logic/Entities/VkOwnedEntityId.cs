namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed record VkOwnedEntityId(
    long OwnerId,
    long Id
);