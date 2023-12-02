using Autofac;
using Module.MusicSourcesStorage.Logic.Factories.Abstract;
using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Logic.Factories;

public sealed class HierarchyBuilderFactory : IHierarchyBuilderFactory
{
    private readonly ILifetimeScope _lifetimeScope;

    public HierarchyBuilderFactory(ILifetimeScope lifetimeScope)
    {
        _lifetimeScope = lifetimeScope;
    }

    public IHierarchyBuilder<TValue, TPathElement> Create<TValue, TPathElement>(
        Func<TValue, IReadOnlyList<TPathElement>> pathElementsFactory,
        IEqualityComparer<TPathElement> pathElementEqualityComparer)
    {
        return _lifetimeScope.Resolve<IHierarchyBuilder<TValue, TPathElement>>(
            new TypedParameter(
                typeof(Func<TValue, IReadOnlyList<TPathElement>>),
                pathElementsFactory
            ),
            new TypedParameter(
                typeof(IEqualityComparer<TPathElement>),
                pathElementEqualityComparer
            )
        );
    }
}