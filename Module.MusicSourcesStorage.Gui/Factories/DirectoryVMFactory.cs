using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;

namespace Module.MusicSourcesStorage.Gui.Factories;

public delegate IDirectoryVM DirectoryVMFactory(
    string name,
    IReadOnlyList<INodeVM> childNodes
);