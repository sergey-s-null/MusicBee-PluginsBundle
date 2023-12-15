using Module.MusicSourcesStorage.Logic.Entities.Abstract;

namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed record Leaf<TValue, TPathElement>(
    IReadOnlyList<TPathElement> Path,
    TValue Value
) : ILeaf<TValue, TPathElement>;