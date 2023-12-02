namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed record VkDocument(
    VkOwnedEntityId Id,
    string Name,
    string Uri,
    long? Size
);