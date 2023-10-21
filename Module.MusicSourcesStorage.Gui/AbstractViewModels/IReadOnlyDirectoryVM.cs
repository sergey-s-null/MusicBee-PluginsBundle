using System.IO;

namespace Module.MusicSourcesStorage.Gui.AbstractViewModels;

public interface IReadOnlyDirectoryVM : INodeVM
{
    bool HasCover { get; }
    Stream? CoverStream { get; }
}