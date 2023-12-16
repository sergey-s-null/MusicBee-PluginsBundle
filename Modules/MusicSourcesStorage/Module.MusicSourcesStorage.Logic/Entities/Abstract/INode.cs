namespace Module.MusicSourcesStorage.Logic.Entities.Abstract;

public interface INode<out TValue, out TPathElement>
{
    IReadOnlyList<TPathElement> Path { get; }
    IReadOnlyList<INode<TValue, TPathElement>> ChildNodes { get; }
    IReadOnlyList<ILeaf<TValue, TPathElement>> Leaves { get; }
}