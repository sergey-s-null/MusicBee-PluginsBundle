using System.Windows.Input;

namespace Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;

public interface ICoverRemovableVM
{
    bool CanRemoveCover { get; }

    ICommand RemoveCover { get; }
}