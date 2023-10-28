using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using Module.MusicSourcesStorage.Gui.Enums;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels.Nodes;

[AddINotifyPropertyChangedInterface]
public sealed class ConnectedMusicFileVM : MusicFileVM, IConnectedMusicFileVM
{
    public MusicFileState State { get; private set; }

    public ConnectedMusicFileVM(string path) : base(path)
    {
        // todo get from api
        State = MusicFileState.NotListened;
    }
}