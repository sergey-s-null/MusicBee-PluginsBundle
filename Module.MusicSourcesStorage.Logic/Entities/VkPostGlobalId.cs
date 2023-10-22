namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed record VkPostGlobalId(
    ulong OwnerId,
    ulong LocalId
);