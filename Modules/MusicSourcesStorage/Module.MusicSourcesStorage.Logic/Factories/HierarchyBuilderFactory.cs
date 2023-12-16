using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Factories.Abstract;
using Module.MusicSourcesStorage.Logic.Services;
using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Logic.Factories;

public sealed class HierarchyBuilderFactory : IHierarchyBuilderFactory
{
    private readonly Func<ILeavesSeparator> _leavesSeparatorProvider;

    public HierarchyBuilderFactory(Func<ILeavesSeparator> leavesSeparatorProvider)
    {
        _leavesSeparatorProvider = leavesSeparatorProvider;
    }

    public IHierarchyBuilder<TValue, TPathElement> Create<TValue, TPathElement>(
        Func<TValue, IReadOnlyList<TPathElement>> pathElementsFactory,
        IEqualityComparer<TPathElement> pathElementEqualityComparer,
        HierarchyBuilderConfiguration configuration)
    {
        return new HierarchyBuilder<TValue, TPathElement>(
            pathElementsFactory,
            new LeavesGroupingService<TValue, TPathElement>(
                configuration,
                _leavesSeparatorProvider(),
                pathElementEqualityComparer
            )
        );
    }
}