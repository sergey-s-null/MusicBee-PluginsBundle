using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels.Nodes;

public sealed class DownloadableDTVM : IDownloadableVM
{
    public bool CanDownload => false;
    public bool IsDownloaded => true;

    public ICommand Download => null!;
}