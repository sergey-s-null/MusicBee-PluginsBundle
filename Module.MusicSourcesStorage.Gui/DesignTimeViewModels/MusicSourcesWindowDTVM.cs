using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Gui.Enums;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels;

public sealed class MusicSourcesWindowDTVM : IMusicSourcesWindowVM
{
    public IList<IMusicSourceVM> MusicSources { get; }
    public IMusicSourceVM? SelectedMusicSource { get; set; }

    public MusicSourcesWindowDTVM()
    {
        var hundred = new List<INodeVM>();
        for (var i = 0; i < 100; i++)
        {
            hundred.Add(new ConnectedMusicFileDTVM($"Song{i}.mp3", MusicFileState.NotListened));
        }

        MusicSources = new List<IMusicSourceVM>
        {
            new MusicSourceDTVM("First", MusicSourceType.Torrent, new INodeVM[]
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
            }),
            new MusicSourceDTVM("2nd", MusicSourceType.VkPost, new INodeVM[]
            {
                new ConnectedMusicFileDTVM("Hello.mp3", MusicFileState.InLibrary),
                new ConnectedMusicFileDTVM("There.mp3", MusicFileState.ListenedAndDeleted)
            }),
            new MusicSourceDTVM("So Deep", MusicSourceType.VkPost, new INodeVM[]
            {
                new ConnectedDirectoryDTVM("Right", new INodeVM[]
                {
                    new ConnectedDirectoryDTVM("Into", new INodeVM[]
                    {
                        new ConnectedDirectoryDTVM("The", new INodeVM[]
                        {
                            new ConnectedDirectoryDTVM("Abyss")
                        })
                    })
                })
            }),
            new MusicSourceDTVM("So BIG", MusicSourceType.Torrent, hundred)
        };

        for (var i = 0; i < 100; i++)
        {
            MusicSources.Add(new MusicSourceDTVM("-Inf", MusicSourceType.Torrent));
        }

        SelectedMusicSource = MusicSources[0];
    }
}