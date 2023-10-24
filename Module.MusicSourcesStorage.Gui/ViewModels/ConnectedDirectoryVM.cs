using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels;

[AddINotifyPropertyChangedInterface]
public sealed class ConnectedDirectoryVM : DirectoryVM, IConnectedDirectoryVM
{
    public ConnectedDirectoryVM(string name, IReadOnlyList<INodeVM> childNodes) : base(name, childNodes)
    {
    }
}