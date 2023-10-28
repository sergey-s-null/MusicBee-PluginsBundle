using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;

namespace Module.MusicSourcesStorage.Gui.Factories;

public delegate IConnectedMusicFileVM ConnectedMusicFileVMFactory(
    string path
);