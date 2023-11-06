using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using Module.MusicSourcesStorage.Gui.DesignTimeViewModels.Nodes;
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
            new ImageFileDTVM("cover.jpg")
        }),
        new DirectoryDTVM("Epic Album 666", new INodeVM[]
        {
            new DirectoryDTVM("Special", new INodeVM[]
            {
                new UnknownFileDTVM("song lyrics.txt"),
                new UnknownFileDTVM("message from author.txt")
            }),
            new MusicFileDTVM("Single.flac"),
            new ImageFileDTVM("cover.png"),
            new ImageFileDTVM("some image.png")
        }),
        new DirectoryDTVM("Just a joke", new INodeVM[]
        {
            new ImageFileDTVM("only-cover.png")
        })
    };

    public static readonly IReadOnlyList<INodeVM> ConnectedAllTypesNodes = new INodeVM[]
    {
        new ConnectedDirectoryDTVM("Album 1", new INodeVM[]
        {
            new ConnectedMusicFileDTVM("Song0.mp3", MusicFileLocation.NotDownloaded, false, true),
            new ConnectedMusicFileDTVM("Song1.mp3", MusicFileLocation.NotDownloaded, false),
            new ConnectedMusicFileDTVM("Song2.mp3", MusicFileLocation.Incoming, false),
            new ConnectedMusicFileDTVM("Song3.mp3", MusicFileLocation.Incoming, true),
            new ConnectedMusicFileDTVM("Song999.mp3", MusicFileLocation.Library, false),
            new ConnectedMusicFileDTVM("Song42.mp3", MusicFileLocation.NotDownloaded, true),
            new ConnectedImageFileDTVM("cover.jpg", true),
            new ConnectedImageFileDTVM("alt_cover.jpg", true, true),
        }, "quad.png", isProcessing: true),
        new ConnectedDirectoryDTVM("Epic Album 666", new INodeVM[]
        {
            new ConnectedDirectoryDTVM("Special", new INodeVM[]
            {
                new ConnectedUnknownFileDTVM("song lyrics.txt", true),
                new ConnectedUnknownFileDTVM("message from author.txt")
            }),
            new ConnectedMusicFileDTVM("Single.flac", MusicFileLocation.NotDownloaded, false),
            new ConnectedImageFileDTVM("cover.png", true),
            new ConnectedImageFileDTVM("some image.png", false)
        }, "vertical.png", true),
        new ConnectedDirectoryDTVM("Just a joke", new INodeVM[]
        {
            new ConnectedImageFileDTVM("only-cover.png", true)
        }, "horizontal.png")
    };

    public static readonly string BigText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.\n" +
                                            "Suspendisse ullamcorper ante lorem, vitae dapibus leo egestas eu.\n" +
                                            "Nulla vitae nunc dignissim, dignissim augue non, ornare neque.\n" +
                                            "Donec vehicula mauris id accumsan rutrum.\n" +
                                            "Donec vel risus dictum, sodales velit sed, dapibus turpis.\n" +
                                            "Sed vitae ipsum nec leo tempor lacinia ac eget ante.\n" +
                                            "In tincidunt euismod nibh.\n" +
                                            "Integer eleifend arcu non arcu eleifend scelerisque.\n" +
                                            "Ut consectetur lacus ut porta maximus.\n" +
                                            "Phasellus lacinia egestas volutpat.\n" +
                                            "Curabitur sed leo non libero auctor condimentum aliquet nec ante.\n" +
                                            "Aenean quis tristique orci.\n" +
                                            "Praesent augue ex, mollis vitae arcu non, accumsan interdum risus.\n" +
                                            "Pellentesque lobortis eu lacus.\n" +
                                            "Maecenas nec mollis felis, laoreet placerat nibh.\n" +
                                            "Cras felis tortor, pellentesque a volutpat ut, eleifend vitae magna.\n" +
                                            "Aliquam non arcu sem.\n" +
                                            "Morbi dictum pharetra metus, porttitor cursus tellus elementum eget.\n" +
                                            "Suspendisse pharetra metus id ipsum scelerisque malesuada.\n" +
                                            "Phasellus at facilisis metus.\n" +
                                            "Nunc laoreet volutpat eros, vel cursus sapien dictum at.\n" +
                                            "Morbi eleifend neque arcu, sit amet volutpat nibh iaculis eu.\n" +
                                            "Maecenas suscipit venenatis vestibulum.\n" +
                                            "Phasellus id massa sit amet mi rhoncus feugiat.\n" +
                                            "Praesent consequat mollis suscipit.\n" +
                                            "Donec nec quam mauris.\n" +
                                            "\n" +
                                            "Morbi venenatis diam vitae lobortis tempus. Curabitur eget pharetra dolor. Sed consequat pretium tellus, id iaculis odio ullamcorper vitae. Vestibulum fringilla diam bibendum sapien consequat, in cursus enim hendrerit. Phasellus nec nunc efficitur, tristique risus vel, dictum justo. In imperdiet quis velit sit amet sollicitudin. Pellentesque tincidunt vitae odio sit amet dapibus.";
}