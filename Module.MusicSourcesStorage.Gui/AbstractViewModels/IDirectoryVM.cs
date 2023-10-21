using System.IO;

namespace Module.MusicSourcesStorage.Gui.AbstractViewModels;

public interface IDirectoryVM : INodeVM
{
    bool HasCover { get; }
    Stream? CoverStream { get; }
}