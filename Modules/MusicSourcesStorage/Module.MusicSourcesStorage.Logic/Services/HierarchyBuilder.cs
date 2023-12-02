using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services;

public sealed class HierarchyBuilder<TValue, TPathElement> : IHierarchyBuilder<TValue, TPathElement>
{
    private readonly Func<TValue, IReadOnlyList<TPathElement>> _pathElementsFactory;
    private readonly IEqualityComparer<TPathElement> _pathElementEqualityComparer;

    public HierarchyBuilder(
        Func<TValue, IReadOnlyList<TPathElement>> pathElementsFactory,
        IEqualityComparer<TPathElement> pathElementEqualityComparer)
    {
        _pathElementsFactory = pathElementsFactory;
        _pathElementEqualityComparer = pathElementEqualityComparer;
    }

    public void Build(
        IReadOnlyList<TValue> values,
        out IReadOnlyList<Node<TValue, TPathElement>> rootNodes,
        out IReadOnlyList<Leaf<TValue, TPathElement>> rootLeaves)
    {
        var flatItems = values
            .Select(x => new Leaf<TValue, TPathElement>(_pathElementsFactory(x), x))
            .ToList();

        Build(Array.Empty<TPathElement>(), flatItems, 0, out rootNodes, out rootLeaves);
    }

    private void Build(
        IReadOnlyList<TPathElement> currentPath,
        IReadOnlyList<Leaf<TValue, TPathElement>> items,
        int level,
        out IReadOnlyList<Node<TValue, TPathElement>> nodes,
        out IReadOnlyList<Leaf<TValue, TPathElement>> leaves)
    {
        DistinguishLeavesAtLevel(items, level, out var notLeaves, out leaves);

        if (notLeaves.Count == 0)
        {
            nodes = Array.Empty<Node<TValue, TPathElement>>();
            return;
        }

        nodes = notLeaves
            .GroupBy(x => x.Path[level], _pathElementEqualityComparer)
            .Select(x => BuildNode(
                currentPath.Append(x.Key).ToList(),
                x.ToList(),
                level
            ))
            .ToList();
    }

    private Node<TValue, TPathElement> BuildNode(
        IReadOnlyList<TPathElement> nodePath,
        IReadOnlyList<Leaf<TValue, TPathElement>> values,
        int level)
    {
        Build(nodePath, values, level + 1, out var nodes, out var leaves);
        return new Node<TValue, TPathElement>(
            nodePath,
            nodes,
            leaves
        );
    }

    private static void DistinguishLeavesAtLevel(
        IReadOnlyList<Leaf<TValue, TPathElement>> items,
        int level,
        out IReadOnlyList<Leaf<TValue, TPathElement>> notLeaves,
        out IReadOnlyList<Leaf<TValue, TPathElement>> leaves)
    {
        var notLeavesInternal = new List<Leaf<TValue, TPathElement>>();
        var leavesInternal = new List<Leaf<TValue, TPathElement>>();

        foreach (var item in items)
        {
            if (IsLeafAtLevel(item, level))
            {
                leavesInternal.Add(item);
            }
            else
            {
                notLeavesInternal.Add(item);
            }
        }

        notLeaves = notLeavesInternal;
        leaves = leavesInternal;
    }

    private static bool IsLeafAtLevel(Leaf<TValue, TPathElement> leaf, int level)
    {
        return level + 1 == leaf.Path.Count;
    }
}