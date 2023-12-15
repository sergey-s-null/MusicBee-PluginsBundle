using Module.MusicSourcesStorage.Logic.Entities.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services.Abstract;

public interface ILeavesSeparator<TValue, TPathElement>
{
    void SeparateLeavesAtLevel(
        IReadOnlyList<ILeaf<TValue, TPathElement>> rawLeaves,
        int level,
        out IReadOnlyList<ILeaf<TValue, TPathElement>> notLeaves,
        out IReadOnlyList<ILeaf<TValue, TPathElement>> leaves
    );
}