using System.IO;
using System.Reflection;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels.Nodes;

public sealed class ConnectedDirectoryDTVM : DirectoryDTVM, IConnectedDirectoryVM
{
    private const string DesignTimeCoversPath =
        $"{nameof(Module)}.{nameof(MusicSourcesStorage)}.{nameof(Gui)}.Resources.DesignTime.Covers";

    public Stream? CoverStream { get; }

    public ConnectedDirectoryDTVM()
        : this("Some connected directory")
    {
    }

    public ConnectedDirectoryDTVM(string name)
        : this(name, Array.Empty<INodeVM>())
    {
    }

    public ConnectedDirectoryDTVM(
        string name,
        IReadOnlyList<INodeVM> childNodes,
        string? coverFileName = null)
        : base(name, childNodes)
    {
        CoverStream = GetCoverStream(coverFileName);
    }

    private static Stream? GetCoverStream(string? coverFileName)
    {
        if (coverFileName is null)
        {
            return null;
        }

        var resourceName = $"{DesignTimeCoversPath}.{coverFileName}";
        return Assembly.GetAssembly(typeof(ConnectedDirectoryDTVM))
            .GetManifestResourceStream(resourceName);
    }
}