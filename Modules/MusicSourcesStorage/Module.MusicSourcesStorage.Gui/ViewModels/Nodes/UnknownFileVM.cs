using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels.Nodes;

[AddINotifyPropertyChangedInterface]
public class UnknownFileVM : FileBaseVM, IUnknownFileVM
{
    public UnknownFileVM(string path) : base(path)
    {
    }
}