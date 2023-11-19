using System.Windows.Input;

namespace Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;

public interface IConnectedImageFileVM : IConnectedNodeVM
{
    bool IsCover { get; }
    bool CanSelectAsCover { get; }
    bool CanRemoveCover { get; }

    ICommand SelectAsCover { get; }
    ICommand RemoveCover { get; }
}