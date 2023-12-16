using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Entities.Abstract;
using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services;

public sealed class HierarchyBuilder<TValue, TPathElement> : IHierarchyBuilder<TValue, TPathElement>
{
    private readonly Func<TValue, IReadOnlyList<TPathElement>> _pathElementsFactory;
    private readonly ILeavesGroupingService<TValue, TPathElement> _leavesGroupingService;

    public HierarchyBuilder(
        Func<TValue, IReadOnlyList<TPathElement>> pathElementsFactory,
        ILeavesGroupingService<TValue, TPathElement> leavesGroupingService,
        HierarchyBuilderConfiguration configuration)
    {
        _pathElementsFactory = pathElementsFactory;
        _leavesGroupingService = leavesGroupingService;
    }

    public void Build(
        IReadOnlyList<TValue> values,
        out IReadOnlyList<INode<TValue, TPathElement>> rootNodes,
        out IReadOnlyList<ILeaf<TValue, TPathElement>> rootLeaves)
    {
        var rawLeaves = values
            .Select(x => new Leaf<TValue, TPathElement>(_pathElementsFactory(x), x))
            .ToList();

        _leavesGroupingService.Group(Array.Empty<TPathElement>(), rawLeaves, out rootNodes, out rootLeaves);
    }
}