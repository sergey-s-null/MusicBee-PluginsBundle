using Module.MusicSourcesStorage.Logic.Entities.Abstract;
using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services;

public sealed class LeavesSeparator<TValue, TPathElement> : ILeavesSeparator<TValue, TPathElement>
{
    public void SeparateLeavesAtLevel(
        IReadOnlyList<ILeaf<TValue, TPathElement>> rawLeaves,
        int level,
        out IReadOnlyList<ILeaf<TValue, TPathElement>> notLeaves,
        out IReadOnlyList<ILeaf<TValue, TPathElement>> leaves)
    {
        var notLeavesInternal = new List<ILeaf<TValue, TPathElement>>();
        var leavesInternal = new List<ILeaf<TValue, TPathElement>>();

        foreach (var rawLeaf in rawLeaves)
        {
            if (IsLeafAtLevel(rawLeaf, level))
            {
                leavesInternal.Add(rawLeaf);
            }
            else
            {
                notLeavesInternal.Add(rawLeaf);
            }
        }

        notLeaves = notLeavesInternal;
        leaves = leavesInternal;
    }

    private static bool IsLeafAtLevel(ILeaf<TValue, TPathElement> leaf, int level)
    {
        return level + 1 == leaf.Path.Count;
    }
}