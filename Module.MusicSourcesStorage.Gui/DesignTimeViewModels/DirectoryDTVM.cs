using System.IO;
using System.Reflection;
using Module.MusicSourcesStorage.Gui.AbstractViewModels;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels;

public sealed class DirectoryDTVM : IDirectoryVM
{
    private const string DesignTimeCoversPath =
        $"{nameof(Module)}.{nameof(MusicSourcesStorage)}.{nameof(Gui)}.Resources.DesignTime.Covers";

    public string Name { get; }
    public IReadOnlyList<INodeVM> ChildNodes { get; }
    public bool HasCover => _coverFileName is not null;
    public Stream? CoverStream => GetCoverStream();

    private readonly string? _coverFileName;

    // ReSharper disable once UnusedMember.Global
    public DirectoryDTVM() : this("SomeDirectory")
    {
    }

    public DirectoryDTVM(string name) : this(name, Array.Empty<INodeVM>())
    {
    }

    public DirectoryDTVM(string name, IReadOnlyList<INodeVM> childNodes, string? coverFileName = null)
    {
        Name = name;
        ChildNodes = childNodes;
        _coverFileName = coverFileName;
    }

    private Stream? GetCoverStream()
    {
        if (_coverFileName is null)
        {
            return null;
        }

        var resourceName = $"{DesignTimeCoversPath}.{_coverFileName}";
        return Assembly.GetAssembly(typeof(DirectoryDTVM)).GetManifestResourceStream(resourceName);
    }
}