namespace Module.MusicSourcesStorage.Logic.Entities.Abstract;

public interface ILeaf<out TValue, out TPathElement>
{
    IReadOnlyList<TPathElement> Path { get; }
    TValue Value { get; }
}