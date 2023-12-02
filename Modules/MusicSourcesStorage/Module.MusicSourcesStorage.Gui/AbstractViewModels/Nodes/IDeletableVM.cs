using System.Windows.Input;

namespace Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;

public interface IDeletableVM
{
    bool CanDelete { get; }
    bool IsDeleted { get; }

    ICommand Delete { get; }
    ICommand DeleteNoPrompt { get; }
}