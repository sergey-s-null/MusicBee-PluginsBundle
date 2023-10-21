using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Gui.Enums;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels;

public static class DesignTimeData
{
    public static readonly IReadOnlyList<INodeVM> NotConnectedAllTypesNodes = new INodeVM[]
    {
        new DirectoryDTVM("Album 1", new INodeVM[]
        {
            new MusicFileDTVM("Song1.mp3"),
            new MusicFileDTVM("Song2.mp3"),
            new MusicFileDTVM("Song999.mp3"),
            new MusicFileDTVM("Song42.mp3"),
            new ImageFileDTVM("cover.jpg", true)
        }, "quad.png"),
        new DirectoryDTVM("Epic Album 666", new INodeVM[]
        {
            new DirectoryDTVM("Special", new INodeVM[]
            {
                new UnknownFileDTVM("song lyrics.txt"),
                new UnknownFileDTVM("message from author.txt")
            }),
            new MusicFileDTVM("Single.flac"),
            new ImageFileDTVM("cover.png", true),
            new ImageFileDTVM("some image.png", false)
        }, "vertical.png"),
        new DirectoryDTVM("Just a joke", new INodeVM[]
        {
            new ImageFileDTVM("only-cover.png", true)
        }, "horizontal.png")
    };

    public static readonly IReadOnlyList<INodeVM> ConnectedAllTypesNodes = new INodeVM[]
    {
        new ConnectedDirectoryDTVM("Album 1", new INodeVM[]
        {
            new ConnectedMusicFileDTVM("Song1.mp3", MusicFileState.NotListened),
            new ConnectedMusicFileDTVM("Song2.mp3", MusicFileState.InIncoming),
            new ConnectedMusicFileDTVM("Song999.mp3", MusicFileState.InLibrary),
            new ConnectedMusicFileDTVM("Song42.mp3", MusicFileState.ListenedAndDeleted),
            new ConnectedImageFileDTVM("cover.jpg", true)
        }, "quad.png"),
        new ConnectedDirectoryDTVM("Epic Album 666", new INodeVM[]
        {
            new ConnectedDirectoryDTVM("Special", new INodeVM[]
            {
                new ConnectedUnknownFileDTVM("song lyrics.txt"),
                new ConnectedUnknownFileDTVM("message from author.txt")
            }),
            new ConnectedMusicFileDTVM("Single.flac", MusicFileState.ListenedAndDeleted),
            new ConnectedImageFileDTVM("cover.png", true),
            new ConnectedImageFileDTVM("some image.png", false)
        }, "vertical.png"),
        new ConnectedDirectoryDTVM("Just a joke", new INodeVM[]
        {
            new ConnectedImageFileDTVM("only-cover.png", true)
        }, "horizontal.png")
    };
}