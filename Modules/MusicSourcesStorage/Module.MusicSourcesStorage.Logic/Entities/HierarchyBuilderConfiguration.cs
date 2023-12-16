using Module.MusicSourcesStorage.Logic.Enums;

namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed record HierarchyBuilderConfiguration(
    LeavesDuplicationResolutionMode LeavesDuplicationResolutionMode = LeavesDuplicationResolutionMode.KeepAll,
    LeafHasNodeNameResolutionMode LeafHasNodeNameResolutionMode = LeafHasNodeNameResolutionMode.KeepAsLeaf
)
{
    public static readonly HierarchyBuilderConfiguration Default = new();
}