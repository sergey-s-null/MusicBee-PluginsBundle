using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels.Nodes;

[AddINotifyPropertyChangedInterface]
public sealed class ConnectedImageFileVM : ImageFileVM, IConnectedImageFileVM
{
    public ConnectedImageFileVM(string path) : base(path)
    {
    }
}