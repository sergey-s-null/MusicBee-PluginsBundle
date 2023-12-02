using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using Module.MusicSourcesStorage.Logic.Entities;

namespace Module.MusicSourcesStorage.Gui.Services.Abstract;

public interface IConnectedFileVMBuilder
{
    IConnectedNodeVM Build(SourceFile sourceFile);
}