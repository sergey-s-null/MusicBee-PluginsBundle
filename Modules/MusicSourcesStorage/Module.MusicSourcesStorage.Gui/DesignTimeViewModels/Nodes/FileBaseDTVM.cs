using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels.Nodes;

public abstract class FileBaseDTVM : INodeVM
{
    public abstract string Name { get; }
    public abstract string Path { get; }

    public bool IsExpanded { get; set; }
    public IReadOnlyList<INodeVM> ChildNodes { get; } = Array.Empty<INodeVM>();
}