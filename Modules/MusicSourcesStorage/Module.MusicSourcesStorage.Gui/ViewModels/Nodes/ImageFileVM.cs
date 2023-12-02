using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels.Nodes;

[AddINotifyPropertyChangedInterface]
public class ImageFileVM : FileBaseVM, IImageFileVM
{
    public ImageFileVM(string path) : base(path)
    {
    }
}