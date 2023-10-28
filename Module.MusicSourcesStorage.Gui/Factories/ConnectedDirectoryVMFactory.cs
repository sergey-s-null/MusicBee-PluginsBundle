using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;

namespace Module.MusicSourcesStorage.Gui.Factories;

public delegate IConnectedDirectoryVM ConnectedDirectoryVMFactory(
    string name,
    IReadOnlyList<INodeVM> childNodes
);