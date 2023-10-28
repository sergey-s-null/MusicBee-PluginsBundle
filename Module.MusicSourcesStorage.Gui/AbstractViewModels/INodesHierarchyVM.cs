using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;

namespace Module.MusicSourcesStorage.Gui.AbstractViewModels;

public interface INodesHierarchyVM
{
    IReadOnlyList<INodeVM> RootNodes { get; }
}