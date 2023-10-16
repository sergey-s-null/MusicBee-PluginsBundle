using Module.MusicSourcesStorage.Gui.AbstractViewModels;

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
            hundred.Add(new FileDTVM($"Song{i}.mp3"));
        }

        MusicSources = new List<IMusicSourceVM>
        {
            new MusicSourceDTVM("First", new INodeVM[]
            {
                new DirectoryDTVM("Album 1", new INodeVM[]
                {
                    new FileDTVM("Song1.mp3"),
                    new FileDTVM("Song2.mp3"),
                    new FileDTVM("Song999.mp3"),
                    new FileDTVM("cover.jpg")
                }),
                new DirectoryDTVM("Epic Album 666", new INodeVM[]
                {
                    new DirectoryDTVM("Special", new INodeVM[]
                    {
                        new FileDTVM("song lyrics.txt"),
                        new FileDTVM("message from author.txt")
                    }),
                    new FileDTVM("Single.flac"),
                    new FileDTVM("cover.png")
                })
            }),
            new MusicSourceDTVM("2nd", new INodeVM[]
            {
                new FileDTVM("Hello.mp3"),
                new FileDTVM("There.mp3")
            }),
            new MusicSourceDTVM("So Deep", new INodeVM[]
            {
                new DirectoryDTVM("Right", new INodeVM[]
                {
                    new DirectoryDTVM("Into", new INodeVM[]
                    {
                        new DirectoryDTVM("The", new INodeVM[]
                        {
                            new DirectoryDTVM("Abyss")
                        })
                    })
                })
            }),
            new MusicSourceDTVM("So BIG", hundred)
        };

        for (var i = 0; i < 100; i++)
        {
            MusicSources.Add(new MusicSourceDTVM("-Inf"));
        }
    }
}