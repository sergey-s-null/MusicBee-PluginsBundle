using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;

namespace Module.MusicSourcesStorage.Gui.Factories;

public delegate IConnectedDirectoryVM ConnectedDirectoryVMFactory(
    int sourceId,
    string path,
    IReadOnlyList<INodeVM> childNodes
);