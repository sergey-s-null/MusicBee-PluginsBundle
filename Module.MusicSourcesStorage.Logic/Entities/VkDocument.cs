namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed record VkDocument(
    long Id,
    long OwnerId,
    string Name,
    string Uri,
    long? Size
);