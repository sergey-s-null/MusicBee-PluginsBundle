namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed record Leaf<TValue, TPathElement>(
    IReadOnlyList<TPathElement> Path,
    TValue Value
);