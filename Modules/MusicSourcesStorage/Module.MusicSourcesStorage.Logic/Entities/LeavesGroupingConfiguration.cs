using Module.MusicSourcesStorage.Logic.Enums;

namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed record LeavesGroupingConfiguration(
    LeavesDuplicationResolutionMode LeavesDuplicationResolutionMode = LeavesDuplicationResolutionMode.KeepAll,
    LeafHasNodeNameResolutionMode LeafHasNodeNameResolutionMode = LeafHasNodeNameResolutionMode.KeepAsLeaf
)
{
    public static readonly LeavesGroupingConfiguration Default = new();
}