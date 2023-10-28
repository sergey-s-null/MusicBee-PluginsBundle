namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed record VkDocumentModel(
    long Id,
    long OwnerId,
    string Name,
    string Uri,
    long? Size
);