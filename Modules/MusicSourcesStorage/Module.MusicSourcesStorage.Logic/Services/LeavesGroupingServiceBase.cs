using Module.MusicSourcesStorage.Logic.Entities.Abstract;
using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services;

public abstract class LeavesGroupingServiceBase<TValue, TPathElement> : ILeavesGroupingService<TValue, TPathElement>
{
    private readonly ILeavesSeparator<TValue, TPathElement> _leavesSeparator;
    private readonly IEqualityComparer<TPathElement> _pathElementEqualityComparer;

    protected LeavesGroupingServiceBase(
        ILeavesSeparator<TValue, TPathElement> leavesSeparator,
        IEqualityComparer<TPathElement> pathElementEqualityComparer)
    {
        _leavesSeparator = leavesSeparator;
        _pathElementEqualityComparer = pathElementEqualityComparer;
    }

    public void Group(IReadOnlyList<TPathElement> basePath, IReadOnlyList<ILeaf<TValue, TPathElement>> rawLeaves,
        out IReadOnlyList<INode<TValue, TPathElement>> nodes, out IReadOnlyList<ILeaf<TValue, TPathElement>> leaves)
    {
        var level = basePath.Count;
        _leavesSeparator.SeparateLeavesAtLevel(
            rawLeaves, level, out var notLeaves, out leaves
        );

        if (notLeaves.Count == 0)
        {
            nodes = Array.Empty<INode<TValue, TPathElement>>();
            return;
        }

        nodes = notLeaves
            .GroupBy(x => x.Path[level], _pathElementEqualityComparer)
            .Select(x => BuildNode(
                basePath.Append(x.Key).ToList(),
                x.ToList()
            ))
            .ToList();
    }

    protected abstract INode<TValue, TPathElement> BuildNode(
        IReadOnlyList<TPathElement> nodePath,
        IReadOnlyList<ILeaf<TValue, TPathElement>> rawLeaves
    );
}