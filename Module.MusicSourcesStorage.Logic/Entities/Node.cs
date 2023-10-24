namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed record Node<TValue, TPathElement>(
    TPathElement PathElement,
    IReadOnlyList<Node<TValue, TPathElement>> ChildNodes,
    IReadOnlyList<Leaf<TValue, TPathElement>> Leaves
);