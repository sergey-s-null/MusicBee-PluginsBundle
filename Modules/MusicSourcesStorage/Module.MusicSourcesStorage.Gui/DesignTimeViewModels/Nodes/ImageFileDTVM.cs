using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels.Nodes;

public class ImageFileDTVM : FileBaseDTVM, IImageFileVM
{
    public override string Name { get; }
    public override string Path { get; }

    public ImageFileDTVM() : this("some/path/to/image.png")
    {
    }

    public ImageFileDTVM(string path)
    {
        Name = System.IO.Path.GetFileName(path);
        Path = path;
    }
}