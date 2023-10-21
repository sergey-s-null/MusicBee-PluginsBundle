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
            hundred.Add(new MusicFileDTVM($"Song{i}.mp3", MusicFileState.NotListened));
        }

        MusicSources = new List<IMusicSourceVM>
        {
            new MusicSourceDTVM("First", MusicSourceType.Torrent, new INodeVM[]
            {
                new ReadOnlyDirectoryDTVM("Album 1", new INodeVM[]
                {
                    new MusicFileDTVM("Song1.mp3", MusicFileState.NotListened),
                    new MusicFileDTVM("Song2.mp3", MusicFileState.InIncoming),
                    new MusicFileDTVM("Song999.mp3", MusicFileState.InLibrary),
                    new MusicFileDTVM("Song42.mp3", MusicFileState.ListenedAndDeleted),
                    new ReadOnlyImageFileDTVM("cover.jpg", true)
                }, "quad.png"),
                new ReadOnlyDirectoryDTVM("Epic Album 666", new INodeVM[]
                {
                    new ReadOnlyDirectoryDTVM("Special", new INodeVM[]
                    {
                        new UnknownFileDTVM("song lyrics.txt"),
                        new UnknownFileDTVM("message from author.txt")
                    }),
                    new MusicFileDTVM("Single.flac", MusicFileState.ListenedAndDeleted),
                    new ReadOnlyImageFileDTVM("cover.png", true),
                    new ReadOnlyImageFileDTVM("some image.png", false)
                }, "vertical.png"),
                new ReadOnlyDirectoryDTVM("Just a joke", new INodeVM[]
                {
                    new ReadOnlyImageFileDTVM("only-cover.png", true)
                }, "horizontal.png")
            }),
            new MusicSourceDTVM("2nd", MusicSourceType.VkPost, new INodeVM[]
            {
                new MusicFileDTVM("Hello.mp3", MusicFileState.InLibrary),
                new MusicFileDTVM("There.mp3", MusicFileState.ListenedAndDeleted)
            }),
            new MusicSourceDTVM("So Deep", MusicSourceType.VkPost, new INodeVM[]
            {
                new ReadOnlyDirectoryDTVM("Right", new INodeVM[]
                {
                    new ReadOnlyDirectoryDTVM("Into", new INodeVM[]
                    {
                        new ReadOnlyDirectoryDTVM("The", new INodeVM[]
                        {
                            new ReadOnlyDirectoryDTVM("Abyss")
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