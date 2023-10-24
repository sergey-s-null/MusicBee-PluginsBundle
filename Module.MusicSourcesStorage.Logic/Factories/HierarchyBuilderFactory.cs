using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Logic.Factories;

public delegate IHierarchyBuilder<TValue, TPathElement> HierarchyBuilderFactory<TValue, TPathElement>(
    Func<TValue, IReadOnlyList<TPathElement>> pathElementsFactory,
    IEqualityComparer<TPathElement> pathElementEqualityComparer
);