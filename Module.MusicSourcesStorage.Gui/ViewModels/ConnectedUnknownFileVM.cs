using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels;

[AddINotifyPropertyChangedInterface]
public sealed class ConnectedUnknownFileVM : UnknownFileVM, IConnectedUnknownFileVM
{
    public ConnectedUnknownFileVM(string name, string path) : base(name, path)
    {
    }
}