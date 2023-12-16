using Module.MusicSourcesStorage.Logic.Entities.Abstract;

namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed record Node<TValue, TPathElement>(
    IReadOnlyList<TPathElement> Path,
    IReadOnlyList<INode<TValue, TPathElement>> ChildNodes,
    IReadOnlyList<ILeaf<TValue, TPathElement>> Leaves
) : INode<TValue, TPathElement>;