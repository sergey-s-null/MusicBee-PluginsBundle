using Module.MusicSourcesStorage.Gui.Enums;

namespace Module.MusicSourcesStorage.Gui.AbstractViewModels;

public interface IMusicSourceVM
{
    string Name { get; }
    MusicSourceType Type { get; }
    INodesHierarchyVM Items { get; }
}