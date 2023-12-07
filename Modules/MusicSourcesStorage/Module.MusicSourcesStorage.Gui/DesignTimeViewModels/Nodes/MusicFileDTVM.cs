using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels.Nodes;

public class MusicFileDTVM : FileBaseDTVM, IMusicFileVM
{
    public override string Name { get; }
    public override string Path { get; }

    public MusicFileDTVM() : this("path/to/indexed_file.jpg")
    {
    }

    public MusicFileDTVM(string path)
    {
        Name = System.IO.Path.GetFileName(path);
        Path = path;
    }
}