using System.Windows.Input;

namespace Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;

public interface IDownloadableVM
{
    bool CanDownload { get; }
    bool IsDownloaded { get; }

    ICommand Download { get; }
}