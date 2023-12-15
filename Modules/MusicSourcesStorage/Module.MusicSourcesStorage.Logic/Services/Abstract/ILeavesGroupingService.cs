using Module.MusicSourcesStorage.Logic.Entities.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services.Abstract;

public interface ILeavesGroupingService<TValue, TPathElement>
{
    /// <param name="basePath">Common path for all <paramref name="rawLeaves"/></param>
    /// <param name="rawLeaves">Not grouped leaves.</param>
    /// <param name="nodes">Grouped nodes.</param>
    /// <param name="leaves">Leaves at <paramref name="basePath"/>.</param>
    void Group(
        IReadOnlyList<TPathElement> basePath,
        IReadOnlyList<ILeaf<TValue, TPathElement>> rawLeaves,
        out IReadOnlyList<INode<TValue, TPathElement>> nodes,
        out IReadOnlyList<ILeaf<TValue, TPathElement>> leaves
    );
}