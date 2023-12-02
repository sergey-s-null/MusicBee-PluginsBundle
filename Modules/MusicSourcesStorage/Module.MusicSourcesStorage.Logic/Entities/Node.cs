namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed record Node<TValue, TPathElement>(
    IReadOnlyList<TPathElement> Path,
    IReadOnlyList<Node<TValue, TPathElement>> ChildNodes,
    IReadOnlyList<Leaf<TValue, TPathElement>> Leaves
);