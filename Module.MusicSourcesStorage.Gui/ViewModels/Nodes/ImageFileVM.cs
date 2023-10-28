using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels.Nodes;

[AddINotifyPropertyChangedInterface]
public class ImageFileVM : FileBaseVM, IImageFileVM
{
    // todo move to connected?
    public bool IsCover { get; }

    public ImageFileVM(string path) : base(path)
    {
        IsCover = false;
    }
}