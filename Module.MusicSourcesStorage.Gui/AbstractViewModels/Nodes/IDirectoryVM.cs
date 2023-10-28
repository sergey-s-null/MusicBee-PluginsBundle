using System.IO;

namespace Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;

public interface IDirectoryVM : INodeVM
{
    bool HasCover { get; }
    Stream? CoverStream { get; }
}