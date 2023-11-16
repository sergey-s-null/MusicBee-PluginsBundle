using System.IO;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels.Nodes;

[AddINotifyPropertyChangedInterface]
public class DirectoryVM : IDirectoryVM
{
    public string Name { get; }

    public IReadOnlyList<INodeVM> ChildNodes { get; }

    public DirectoryVM(string path, IReadOnlyList<INodeVM> childNodes)
    {
        Name = Path.GetFileName(path);
        ChildNodes = childNodes;
    }
}