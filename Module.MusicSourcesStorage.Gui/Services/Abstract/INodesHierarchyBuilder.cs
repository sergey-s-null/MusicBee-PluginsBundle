using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Logic.Entities;

namespace Module.MusicSourcesStorage.Gui.Services.Abstract;

public interface INodesHierarchyBuilder
{
    INodesHierarchyVM Build(IReadOnlyList<MusicSourceFile> files);
}