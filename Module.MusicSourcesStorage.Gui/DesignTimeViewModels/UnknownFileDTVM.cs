using Module.MusicSourcesStorage.Gui.AbstractViewModels;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels;

public sealed class UnknownFileDTVM : IUnknownFileVM
{
    public string Name { get; }
    public string Path { get; }
    public IList<INodeVM> ChildNodes { get; } = Array.Empty<INodeVM>();

    // ReSharper disable once UnusedMember.Global
    public UnknownFileDTVM() : this("some/path/to/unknown_file")
    {
    }

    public UnknownFileDTVM(string path)
    {
        Name = System.IO.Path.GetFileName(path);
        Path = path;
    }
}