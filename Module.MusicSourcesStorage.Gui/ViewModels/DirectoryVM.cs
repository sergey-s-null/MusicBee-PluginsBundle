using System.IO;
using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels;

[AddINotifyPropertyChangedInterface]
public class DirectoryVM : IDirectoryVM
{
    public string Name { get; }

    // todo move to connected?
    public bool HasCover { get; }
    public Stream? CoverStream { get; }

    public IReadOnlyList<INodeVM> ChildNodes { get; }

    public DirectoryVM(string name, IReadOnlyList<INodeVM> childNodes)
    {
        Name = name;
        HasCover = false;
        CoverStream = null;
        ChildNodes = childNodes;
    }
}