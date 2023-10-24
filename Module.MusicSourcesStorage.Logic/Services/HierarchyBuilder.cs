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
        IReadOnlyList<TValue> flatItems,
        out IReadOnlyList<Node<TValue, TPathElement>> rootNodes,
        out IReadOnlyList<Leaf<TValue, TPathElement>> rootLeaves)
    {
        var values = flatItems
            .Select(BuildPathElements)
            .ToList();

        Build(values, 0, out rootNodes, out rootLeaves);
    }

    private ValueWithPathElements<TValue, TPathElement> BuildPathElements(TValue value)
    {
        return new ValueWithPathElements<TValue, TPathElement>(value, _pathElementsFactory(value));
    }

    private void Build(
        IReadOnlyList<ValueWithPathElements<TValue, TPathElement>> values,
        int level,
        out IReadOnlyList<Node<TValue, TPathElement>> nodes,
        out IReadOnlyList<Leaf<TValue, TPathElement>> leaves)
    {
        DistinguishLeaves(values, level, out var notLeaves, out leaves);

        if (notLeaves.Count == 0)
        {
            nodes = Array.Empty<Node<TValue, TPathElement>>();
            return;
        }

        nodes = notLeaves
            .GroupBy(x => x.PathElements[level], _pathElementEqualityComparer)
            .Select(x => BuildNode(x.Key, x.ToList(), level))
            .ToList();
    }

    private Node<TValue, TPathElement> BuildNode(
        TPathElement pathElement,
        IReadOnlyList<ValueWithPathElements<TValue, TPathElement>> values,
        int level)
    {
        Build(values, level + 1, out var nodes, out var leaves);
        return new Node<TValue, TPathElement>(
            pathElement,
            nodes,
            leaves
        );
    }

    private static void DistinguishLeaves(
        IReadOnlyList<ValueWithPathElements<TValue, TPathElement>> values,
        int level,
        out IReadOnlyList<ValueWithPathElements<TValue, TPathElement>> notLeaves,
        out IReadOnlyList<Leaf<TValue, TPathElement>> leaves)
    {
        var notLeavesInternal = new List<ValueWithPathElements<TValue, TPathElement>>();
        var leavesInternal = new List<Leaf<TValue, TPathElement>>();

        foreach (var value in values)
        {
            if (IsLeaf(value, level))
            {
                leavesInternal.Add(new Leaf<TValue, TPathElement>(
                    value.PathElements[level],
                    value.Value
                ));
            }
            else
            {
                notLeavesInternal.Add(value);
            }
        }

        notLeaves = notLeavesInternal;
        leaves = leavesInternal;
    }

    private static bool IsLeaf(ValueWithPathElements<TValue, TPathElement> value, int level)
    {
        return level + 1 == value.PathElements.Count;
    }
}