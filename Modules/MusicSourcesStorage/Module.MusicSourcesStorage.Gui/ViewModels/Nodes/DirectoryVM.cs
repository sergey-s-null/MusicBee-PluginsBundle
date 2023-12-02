using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels.Nodes;

[AddINotifyPropertyChangedInterface]
public class DirectoryVM : IDirectoryVM
{
    public string Name { get; }
    public string Path { get; }

    public IReadOnlyList<INodeVM> ChildNodes { get; }

    public DirectoryVM(string path, IReadOnlyList<INodeVM> childNodes)
    {
        Name = System.IO.Path.GetFileName(path);
        Path = path;
        ChildNodes = childNodes;
    }
}