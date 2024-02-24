using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels.Nodes;

[AddINotifyPropertyChangedInterface]
public class ImageFileVM : FileBaseVM, IImageFileVM
{
    public override string Name { get; }
    public override string Path { get; }

    public ImageFileVM(string path)
    {
        Name = System.IO.Path.GetFileName(path);
        Path = path;
    }
}