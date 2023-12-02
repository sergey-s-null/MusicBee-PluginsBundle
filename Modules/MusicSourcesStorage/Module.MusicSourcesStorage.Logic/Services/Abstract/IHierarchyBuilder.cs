using Module.MusicSourcesStorage.Logic.Entities;

namespace Module.MusicSourcesStorage.Logic.Services.Abstract;

public interface IHierarchyBuilder<TValue, TPathElement>
{
    void Build(
        IReadOnlyList<TValue> values,
        out IReadOnlyList<Node<TValue, TPathElement>> rootNodes,
        out IReadOnlyList<Leaf<TValue, TPathElement>> rootLeaves
    );
}