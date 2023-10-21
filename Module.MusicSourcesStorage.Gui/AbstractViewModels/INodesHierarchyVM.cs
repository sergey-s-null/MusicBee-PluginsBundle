namespace Module.MusicSourcesStorage.Gui.AbstractViewModels;

public interface INodesHierarchyVM
{
    IReadOnlyList<INodeVM> RootNodes { get; }
}