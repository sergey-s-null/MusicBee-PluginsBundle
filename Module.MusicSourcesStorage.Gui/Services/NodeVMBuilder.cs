using System.IO;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using Module.MusicSourcesStorage.Gui.Factories.Abstract;
using Module.MusicSourcesStorage.Gui.Services.Abstract;
using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Enums;

namespace Module.MusicSourcesStorage.Gui.Services;

public sealed class NodeVMBuilder : INodeVMBuilder
{
    private readonly INodeVMFactory _nodeVMFactory;

    public NodeVMBuilder(INodeVMFactory nodeVMFactory)
    {
        _nodeVMFactory = nodeVMFactory;
    }

    public INodeVM BuildLeaf(MusicSourceFile file)
    {
        return file.Type switch
        {
            FileType.MusicFile => _nodeVMFactory.CreateMusicFileVM(Path.GetFileName(file.Path), file.Path),
            FileType.Image => _nodeVMFactory.CreateImageFileVM(Path.GetFileName(file.Path), file.Path),
            FileType.Unknown => _nodeVMFactory.CreateUnknownFileVM(Path.GetFileName(file.Path), file.Path),
            _ => throw new ArgumentOutOfRangeException(
                nameof(file.Type),
                file.Type,
                "File type is unknown (Not \"Unknown\" type)."
            )
        };
    }
}