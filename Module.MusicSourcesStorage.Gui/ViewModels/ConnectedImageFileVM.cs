using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels;

[AddINotifyPropertyChangedInterface]
public sealed class ConnectedImageFileVM : ImageFileVM, IConnectedImageFileVM
{
    public ConnectedImageFileVM(string name, string path) : base(name, path)
    {
    }
}