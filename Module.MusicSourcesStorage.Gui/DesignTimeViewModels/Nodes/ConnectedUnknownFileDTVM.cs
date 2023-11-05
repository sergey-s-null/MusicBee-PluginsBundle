using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels.Nodes;

public sealed class ConnectedUnknownFileDTVM : UnknownFileDTVM, IConnectedUnknownFileVM
{
    public bool CanDownload => !IsDownloaded;
    public bool IsDownloaded => false;

    public ICommand Download => null!;
    public ICommand Delete => null!;

    public ConnectedUnknownFileDTVM()
    {
    }

    public ConnectedUnknownFileDTVM(string path) : base(path)
    {
    }
}