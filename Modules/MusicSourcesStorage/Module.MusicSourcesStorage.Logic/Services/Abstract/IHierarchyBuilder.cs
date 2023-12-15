using Module.MusicSourcesStorage.Logic.Entities.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services.Abstract;

public interface IHierarchyBuilder<TValue, TPathElement>
{
    void Build(
        IReadOnlyList<TValue> values,
        out IReadOnlyList<INode<TValue, TPathElement>> rootNodes,
        out IReadOnlyList<ILeaf<TValue, TPathElement>> rootLeaves
    );
}