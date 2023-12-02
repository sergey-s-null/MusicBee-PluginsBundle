using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using Module.MusicSourcesStorage.Logic.Entities;

namespace Module.MusicSourcesStorage.Gui.Services.Abstract;

public interface IFileVMBuilder
{
    INodeVM Build(SourceFile sourceFile);
}