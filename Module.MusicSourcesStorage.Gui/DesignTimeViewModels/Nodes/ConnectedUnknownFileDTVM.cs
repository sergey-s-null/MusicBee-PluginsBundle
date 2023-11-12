using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels.Nodes;

public sealed class ConnectedUnknownFileDTVM : UnknownFileDTVM, IConnectedUnknownFileVM
{
    public bool IsProcessing { get; }

    public bool CanDownload => !IsDownloaded;
    public bool IsDownloaded { get; }

    public bool CanDelete => false;
    public bool IsDeleted => true;

    public ICommand Download => null!;
    public ICommand Delete => null!;
    public ICommand DeleteNoPrompt => null!;

    public ConnectedUnknownFileDTVM()
    {
    }

    public ConnectedUnknownFileDTVM(
        string path,
        bool isDownloaded = false,
        bool isProcessing = false)
        : base(path)
    {
        IsDownloaded = isDownloaded;
        IsProcessing = isProcessing;
    }
}