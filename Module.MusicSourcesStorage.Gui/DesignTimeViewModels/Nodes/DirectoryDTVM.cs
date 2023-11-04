using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels.Nodes;

public class DirectoryDTVM : IDirectoryVM
{
    public string Name { get; }
    public IReadOnlyList<INodeVM> ChildNodes { get; }

    public DirectoryDTVM() : this("SomeDirectory")
    {
    }

    public DirectoryDTVM(string name) : this(name, Array.Empty<INodeVM>())
    {
    }

    public DirectoryDTVM(string name, IReadOnlyList<INodeVM> childNodes)
    {
        Name = name;
        ChildNodes = childNodes;
    }
}