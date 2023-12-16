using System.Text;
using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Entities.Abstract;
using Module.MusicSourcesStorage.Logic.Enums;
using Module.MusicSourcesStorage.Logic.Exceptions;
using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services;

public abstract class LeavesGroupingServiceBase<TValue, TPathElement> : ILeavesGroupingService<TValue, TPathElement>
{
    private readonly LeavesDuplicationResolutionMode _leavesDuplicationResolutionMode;
    private readonly LeafHasNodeNameResolutionMode _leafHasNodeNameResolutionMode;
    private readonly ILeavesSeparator _leavesSeparator;
    private readonly IEqualityComparer<TPathElement> _pathElementEqualityComparer;

    protected LeavesGroupingServiceBase(
        LeavesGroupingConfiguration configuration,
        ILeavesSeparator leavesSeparator,
        IEqualityComparer<TPathElement> pathElementEqualityComparer)
    {
        _leavesDuplicationResolutionMode = configuration.LeavesDuplicationResolutionMode;
        _leafHasNodeNameResolutionMode = configuration.LeafHasNodeNameResolutionMode;
        _leavesSeparator = leavesSeparator;
        _pathElementEqualityComparer = pathElementEqualityComparer;
    }

    public void Group(
        IReadOnlyList<ILeaf<TValue, TPathElement>> rawLeaves,
        out IReadOnlyList<INode<TValue, TPathElement>> nodes,
        out IReadOnlyList<ILeaf<TValue, TPathElement>> leaves)
    {
        Group(Array.Empty<TPathElement>(), rawLeaves, out nodes, out leaves);
    }

    public void Group(
        IReadOnlyList<TPathElement> basePath,
        IReadOnlyList<ILeaf<TValue, TPathElement>> rawLeaves,
        out IReadOnlyList<INode<TValue, TPathElement>> nodes,
        out IReadOnlyList<ILeaf<TValue, TPathElement>> leaves)
    {
        var level = basePath.Count;
        _leavesSeparator.SeparateLeavesAtLevel(
            rawLeaves, level, out var notLeaves, out leaves
        );

        leaves = ResolveLeavesDuplications(leaves, level);
        leaves = ResolveLeavesHavingNodeNames(leaves, notLeaves, level);

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

    /// <returns>Resolved leaves.</returns>
    private IReadOnlyList<ILeaf<TValue, TPathElement>> ResolveLeavesDuplications(
        IReadOnlyList<ILeaf<TValue, TPathElement>> leaves,
        int level)
    {
        switch (_leavesDuplicationResolutionMode)
        {
            case LeavesDuplicationResolutionMode.KeepAll:
                return leaves;
            case LeavesDuplicationResolutionMode.TakeFirst:
                return RemoveDuplications(leaves, level);
            case LeavesDuplicationResolutionMode.ThrowException:
                ThrowOnLeavesDuplication(leaves, level);
                return leaves;
            default:
                throw new ArgumentOutOfRangeException(
                    nameof(_leavesDuplicationResolutionMode),
                    _leavesDuplicationResolutionMode,
                    @"Unsupported leaves duplication resolution mode."
                );
        }
    }

    /// <returns>Resolved leaves.</returns>
    private IReadOnlyList<ILeaf<TValue, TPathElement>> ResolveLeavesHavingNodeNames(
        IReadOnlyList<ILeaf<TValue, TPathElement>> leaves,
        IReadOnlyList<ILeaf<TValue, TPathElement>> notLeaves,
        int level)
    {
        switch (_leafHasNodeNameResolutionMode)
        {
            case LeafHasNodeNameResolutionMode.KeepAsLeaf:
                return leaves;
            case LeafHasNodeNameResolutionMode.SkipLeaf:
                return RemoveLeavesHavingNodeNames(leaves, notLeaves, level);
            case LeafHasNodeNameResolutionMode.ThrowException:
                ThrowOnLeafHasNodeName(leaves, notLeaves, level);
                return leaves;
            default:
                throw new ArgumentOutOfRangeException(
                    nameof(_leafHasNodeNameResolutionMode),
                    _leafHasNodeNameResolutionMode,
                    @"Unsupported leaf has node name resolution mode."
                );
        }
    }

    private IReadOnlyList<ILeaf<TValue, TPathElement>> RemoveDuplications(
        IReadOnlyList<ILeaf<TValue, TPathElement>> leaves,
        int level)
    {
        var leafNames = new HashSet<TPathElement>(_pathElementEqualityComparer);

        var result = new List<ILeaf<TValue, TPathElement>>(leaves.Count);

        foreach (var leaf in leaves)
        {
            var leafName = leaf.Path[level];
            if (!leafNames.Contains(leafName))
            {
                result.Add(leaf);
            }

            leafNames.Add(leafName);
        }

        return result;
    }

    private static void ThrowOnLeavesDuplication(IReadOnlyList<ILeaf<TValue, TPathElement>> leaves, int level)
    {
        var leavesByName = new Dictionary<TPathElement, ILeaf<TValue, TPathElement>>();

        foreach (var leaf in leaves)
        {
            var leafName = leaf.Path[level];
            if (leavesByName.TryGetValue(leafName, out var duplicate))
            {
                throw new LeavesDuplicationException(GetLeavesDuplicationMessage(duplicate, leaf));
            }

            leavesByName[leafName] = leaf;
        }
    }

    private IReadOnlyList<ILeaf<TValue, TPathElement>> RemoveLeavesHavingNodeNames(
        IReadOnlyList<ILeaf<TValue, TPathElement>> leaves,
        IReadOnlyList<ILeaf<TValue, TPathElement>> notLeaves,
        int level)
    {
        var nodeNames = notLeaves
            .Select(x => x.Path[level])
            .ToHashSet(_pathElementEqualityComparer);

        return leaves
            .Where(x => !nodeNames.Contains(x.Path[level]))
            .ToList();
    }

    private void ThrowOnLeafHasNodeName(
        IReadOnlyList<ILeaf<TValue, TPathElement>> leaves,
        IReadOnlyList<ILeaf<TValue, TPathElement>> notLeaves,
        int level)
    {
        var notLeavesByNodeName = notLeaves.ToDictionary(
            keySelector: x => x.Path[level],
            elementSelector: x => x,
            _pathElementEqualityComparer
        );

        foreach (var leaf in leaves)
        {
            var leafName = leaf.Path[level];
            if (notLeavesByNodeName.TryGetValue(leafName, out var notLeaf))
            {
                throw new LeafHasNodeNameException(GetLeafHasNodeNameMessage(leaf, notLeaf));
            }
        }
    }

    private static string GetLeavesDuplicationMessage(
        ILeaf<TValue, TPathElement> first,
        ILeaf<TValue, TPathElement> second)
    {
        return $"Found leaves duplication.\n" +
               $"First:\n" +
               $"    Value: {first.Value}\n" +
               $"    Path: {FormatPath(first.Path)}\n" +
               $"Second:\n" +
               $"    Value: {second.Value}\n" +
               $"    Path: {FormatPath(second.Path)}";
    }

    private static string GetLeafHasNodeNameMessage(
        ILeaf<TValue, TPathElement> leaf,
        ILeaf<TValue, TPathElement> notLeaf)
    {
        return $"Found leaf with name equal to node name.\n" +
               $"Leaf:\n" +
               $"    Value: {leaf.Value}\n" +
               $"    Path: {FormatPath(leaf.Path)}\n" +
               $"Node:\n" +
               $"    Value: {notLeaf.Value}\n" +
               $"    Path: {FormatPath(notLeaf.Path)}";
    }

    private static string FormatPath(IReadOnlyList<TPathElement> path)
    {
        var builder = new StringBuilder();

        builder.Append("[");
        for (var i = 0; i < path.Count; i++)
        {
            if (i > 0)
            {
                builder.Append(", ");
            }

            builder.Append(path[i]);
        }

        builder.Append("]");
        return builder.ToString();
    }
}