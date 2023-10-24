namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed record ValueWithPathElements<TValue, TPathElement>(
    TValue Value,
    IReadOnlyList<TPathElement> PathElements
);