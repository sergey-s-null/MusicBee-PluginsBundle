using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels.Nodes;

public sealed class ConnectedImageFileDTVM : ImageFileDTVM, IConnectedImageFileVM
{
    public bool IsProcessing { get; }

    public bool CanDownload => !IsDownloaded;
    public bool IsDownloaded { get; }

    public bool CanDelete => true;
    public bool IsDeleted => false;

    public bool IsCover => false;

    public ICommand Download => null!;
    public ICommand Delete => null!;
    public ICommand DeleteNoPrompt => null!;
    public ICommand SelectAsCover => null!;

    public ConnectedImageFileDTVM()
    {
    }

    public ConnectedImageFileDTVM(
        string path,
        bool isDownloaded = false,
        bool isProcessing = false)
        : base(path)
    {
        IsDownloaded = isDownloaded;
        IsProcessing = isProcessing;
    }
}