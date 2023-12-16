namespace Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;

public interface INodeVM
{
    string Name { get; }
    string Path { get; }

    bool IsExpanded { get; set; }

    IReadOnlyList<INodeVM> ChildNodes { get; }
}