using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Entities.Abstract;
using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services;

public sealed class LeavesGroupingService<TValue, TPathElement> : LeavesGroupingServiceBase<TValue, TPathElement>
{
    public LeavesGroupingService(
        ILeavesSeparator<TValue, TPathElement> leavesSeparator,
        IEqualityComparer<TPathElement> pathElementEqualityComparer)
        : base(leavesSeparator, pathElementEqualityComparer)
    {
    }

    protected override INode<TValue, TPathElement> BuildNode(
        IReadOnlyList<TPathElement> nodePath,
        IReadOnlyList<ILeaf<TValue, TPathElement>> rawLeaves)
    {
        Group(nodePath, rawLeaves, out var nodes, out var leaves);
        return new Node<TValue, TPathElement>(
            nodePath,
            nodes,
            leaves
        );
    }
}