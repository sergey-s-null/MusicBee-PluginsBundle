using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using Module.MusicSourcesStorage.Gui.Services.Abstract;
using Module.MusicSourcesStorage.Gui.ViewModels.Nodes;
using Module.MusicSourcesStorage.Logic.Entities;

namespace Module.MusicSourcesStorage.Gui.Services;

public sealed class NotConnectedFileVMBuilder : IFileVMBuilder
{
    public IFileVM Build(SourceFile sourceFile)
    {
        return sourceFile switch
        {
            MusicFile musicFile => Build(musicFile),
            ImageFile imageFile => Build(imageFile),
            UnknownFile unknownFile => Build(unknownFile),
            _ => throw new ArgumentOutOfRangeException(nameof(sourceFile))
        };
    }

    private static IMusicFileVM Build(MusicFile musicFile)
    {
        return new MusicFileVM(musicFile.Path);
    }

    private static IImageFileVM Build(ImageFile imageFile)
    {
        return new ImageFileVM(imageFile.Path);
    }

    private static IUnknownFileVM Build(UnknownFile unknownFile)
    {
        return new UnknownFileVM(unknownFile.Path);
    }
}