using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Logic.Factories.Abstract;

public interface IHierarchyBuilderFactory
{
    IHierarchyBuilder<TValue, TPathElement> Create<TValue, TPathElement>(
        Func<TValue, IReadOnlyList<TPathElement>> pathElementsFactory,
        IEqualityComparer<TPathElement> pathElementEqualityComparer
    );
}