using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Entities.Abstract;
using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services;

public sealed class LazyLeavesGroupingService<TValue, TPathElement> : LeavesGroupingServiceBase<TValue, TPathElement>
{
    public LazyLeavesGroupingService(
        HierarchyBuilderConfiguration configuration,
        ILeavesSeparator leavesSeparator,
        IEqualityComparer<TPathElement> pathElementEqualityComparer)
        : base(configuration, leavesSeparator, pathElementEqualityComparer)
    {
    }

    protected override INode<TValue, TPathElement> BuildNode(
        IReadOnlyList<TPathElement> nodePath,
        IReadOnlyList<ILeaf<TValue, TPathElement>> rawLeaves)
    {
        return new LazyNode<TValue, TPathElement>(
            nodePath,
            rawLeaves,
            leavesGroupingService: this
        );
    }
}