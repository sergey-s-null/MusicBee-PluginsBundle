using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Factories.Abstract;
using Module.MusicSourcesStorage.Logic.Services;
using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Logic.Factories;

public sealed class LazyHierarchyBuilderFactory : IHierarchyBuilderFactory
{
    private readonly Func<ILeavesSeparator> _leavesSeparatorProvider;

    public LazyHierarchyBuilderFactory(Func<ILeavesSeparator> leavesSeparatorProvider)
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
            new LazyLeavesGroupingService<TValue, TPathElement>(
                configuration,
                _leavesSeparatorProvider(),
                pathElementEqualityComparer
            )
        );
    }
}