using Module.MusicSourcesStorage.Logic.Entities.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services.Abstract;

public interface ILeavesSeparator
{
    void SeparateLeavesAtLevel<TValue, TPathElement>(
        IReadOnlyList<ILeaf<TValue, TPathElement>> rawLeaves,
        int level,
        out IReadOnlyList<ILeaf<TValue, TPathElement>> notLeaves,
        out IReadOnlyList<ILeaf<TValue, TPathElement>> leaves
    );
}