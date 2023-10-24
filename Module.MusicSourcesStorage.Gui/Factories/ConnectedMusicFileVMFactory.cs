using Module.MusicSourcesStorage.Gui.AbstractViewModels;

namespace Module.MusicSourcesStorage.Gui.Factories;

public delegate IConnectedMusicFileVM ConnectedMusicFileVMFactory(string name, string path);