using Module.MusicSourcesStorage.Gui.AbstractViewModels;

namespace Module.MusicSourcesStorage.Gui.Factories;

public delegate IConnectedDirectoryVM ConnectedDirectoryVMFactory(
    string name,
    IReadOnlyList<INodeVM> childNodes
);