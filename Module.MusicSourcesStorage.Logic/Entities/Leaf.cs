namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed record Leaf<TValue, TPathElement>(
    TPathElement PathElement,
    TValue Value
);