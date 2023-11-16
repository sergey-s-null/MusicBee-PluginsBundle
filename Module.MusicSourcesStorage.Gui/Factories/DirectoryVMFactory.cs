using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;

namespace Module.MusicSourcesStorage.Gui.Factories;

public delegate IDirectoryVM DirectoryVMFactory(
    string path,
    IReadOnlyList<INodeVM> childNodes
);