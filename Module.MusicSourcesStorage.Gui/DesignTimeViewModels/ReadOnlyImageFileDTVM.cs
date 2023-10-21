using Module.MusicSourcesStorage.Gui.AbstractViewModels;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels;

public sealed class ReadOnlyImageFileDTVM : IReadOnlyImageFileVM
{
    public string Name { get; }
    public string Path { get; }
    public bool IsCover { get; }
    public IReadOnlyList<INodeVM> ChildNodes { get; } = Array.Empty<INodeVM>();

    // ReSharper disable once UnusedMember.Global
    public ReadOnlyImageFileDTVM() : this("some/path/to/image.png", true)
    {
    }

    public ReadOnlyImageFileDTVM(string path, bool isCover)
    {
        Name = System.IO.Path.GetFileName(path);
        Path = path;
        IsCover = isCover;
    }
}