using System.Windows.Input;
using Module.MusicSourcesStorage.Logic.Enums;

namespace Module.MusicSourcesStorage.Gui.AbstractViewModels;

public interface IMusicSourceVM
{
    string Name { get; }
    MusicSourceType Type { get; }
    INodesHierarchyVM Items { get; }

    ICommand Edit { get; }
    ICommand Delete { get; }
}