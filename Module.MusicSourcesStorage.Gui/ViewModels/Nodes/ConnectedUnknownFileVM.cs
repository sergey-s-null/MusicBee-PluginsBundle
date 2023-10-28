using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels.Nodes;

[AddINotifyPropertyChangedInterface]
public sealed class ConnectedUnknownFileVM : UnknownFileVM, IConnectedUnknownFileVM
{
    public ConnectedUnknownFileVM(string path) : base(path)
    {
    }
}