using System.IO;
using System.Reflection;
using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels.Nodes;

public sealed class ConnectedDirectoryDTVM : DirectoryDTVM, IConnectedDirectoryVM
{
    private const string DesignTimeCoversPath =
        $"{nameof(Module)}.{nameof(MusicSourcesStorage)}.{nameof(Gui)}.Resources.DesignTime.Covers";

    public bool CanDownload => !IsDownloaded;
    public bool IsDownloaded => false;

    public bool CanDelete => false;
    public bool IsDeleted => true;

    public bool IsListened => false;

    public bool HasDownloadedAndNotAttachedToLibraryFiles { get; }

    public Stream? CoverStream { get; }

    public ICommand Download => null!;
    public ICommand Delete => null!;
    public ICommand MarkAsListened => null!;
    public ICommand MarkAsNotListened => null!;

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
        string? coverFileName = null,
        bool hasDownloadedAndNotAttachedToLibraryFiles = false)
        : base(name, childNodes)
    {
        CoverStream = GetCoverStream(coverFileName);
        HasDownloadedAndNotAttachedToLibraryFiles = hasDownloadedAndNotAttachedToLibraryFiles;
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