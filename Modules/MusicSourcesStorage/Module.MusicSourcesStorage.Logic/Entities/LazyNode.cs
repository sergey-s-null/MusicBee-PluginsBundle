using Module.MusicSourcesStorage.Logic.Entities.Abstract;
using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed class LazyNode<TValue, TPathElement> : INode<TValue, TPathElement>
{
    public IReadOnlyList<TPathElement> Path { get; }
    public IReadOnlyList<INode<TValue, TPathElement>> ChildNodes => _nodesAndLeaves.Value.Nodes;
    public IReadOnlyList<ILeaf<TValue, TPathElement>> Leaves => _nodesAndLeaves.Value.Leaves;

    private readonly ILeavesGroupingService<TValue, TPathElement> _leavesGroupingService;

    private readonly IReadOnlyList<ILeaf<TValue, TPathElement>> _rawLeaves;
    private readonly Lazy<NodesAndLeaves> _nodesAndLeaves;

    public LazyNode(
        IReadOnlyList<TPathElement> path,
        IReadOnlyList<ILeaf<TValue, TPathElement>> rawLeaves,
        ILeavesGroupingService<TValue, TPathElement> leavesGroupingService)
    {
        _leavesGroupingService = leavesGroupingService;

        Path = path;
        _rawLeaves = rawLeaves;
        _nodesAndLeaves = new Lazy<NodesAndLeaves>(CalculateNodesAndLeaves);
    }

    private NodesAndLeaves CalculateNodesAndLeaves()
    {
        _leavesGroupingService.Group(Path, _rawLeaves, out var nodes, out var leaves);
        return new NodesAndLeaves(nodes, leaves);
    }

    private sealed record NodesAndLeaves(
        IReadOnlyList<INode<TValue, TPathElement>> Nodes,
        IReadOnlyList<ILeaf<TValue, TPathElement>> Leaves
    );
}