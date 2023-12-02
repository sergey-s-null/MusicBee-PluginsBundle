using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels.Nodes;

[AddINotifyPropertyChangedInterface]
public class MusicFileVM : FileBaseVM, IMusicFileVM
{
    public MusicFileVM(string path) : base(path)
    {
    }
}