using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels.Nodes;

public class DirectoryDTVM : IDirectoryVM
{
    public string Name { get; }
    public string Path { get; }

    public bool IsExpanded { get; set; }

    public IReadOnlyList<INodeVM> ChildNodes { get; }

    public DirectoryDTVM() : this("path/to/SomeDirectory")
    {
    }

    public DirectoryDTVM(string path) : this(path, Array.Empty<INodeVM>())
    {
    }

    public DirectoryDTVM(string path, IReadOnlyList<INodeVM> childNodes)
    {
        Name = System.IO.Path.GetFileName(path);
        Path = path;
        ChildNodes = childNodes;
    }
}