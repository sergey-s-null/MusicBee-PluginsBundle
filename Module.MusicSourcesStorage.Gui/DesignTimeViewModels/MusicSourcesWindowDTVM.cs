using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using Module.MusicSourcesStorage.Gui.DesignTimeViewModels.Nodes;
using Module.MusicSourcesStorage.Gui.Enums;
using Module.MusicSourcesStorage.Logic.Enums;

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
            hundred.Add(new ConnectedMusicFileDTVM($"Song{i}.mp3", MusicFileLocation.NotDownloaded));
        }

        MusicSources = new List<IMusicSourceVM>
        {
            new MusicSourceDTVM("First", MusicSourceType.Torrent, NodesHierarchyDTVM.ConnectedAllTypes),
            new MusicSourceDTVM("NOT CONNECTED", MusicSourceType.Torrent, NodesHierarchyDTVM.NotConnectedAllTypes),
            new MusicSourceDTVM("2nd", MusicSourceType.VkPostWithArchive, new NodesHierarchyDTVM(new INodeVM[]
            {
                new ConnectedMusicFileDTVM("Hello.mp3", MusicFileLocation.Library),
                new ConnectedMusicFileDTVM("There.mp3", MusicFileLocation.NotDownloaded)
            })),
            new MusicSourceDTVM("So Deep", MusicSourceType.VkPostWithArchive, new NodesHierarchyDTVM(new INodeVM[]
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
            })),
            new MusicSourceDTVM("So BIG", MusicSourceType.Torrent, new NodesHierarchyDTVM(hundred))
        };

        for (var i = 0; i < 100; i++)
        {
            MusicSources.Add(new MusicSourceDTVM("-Inf", MusicSourceType.Torrent));
        }

        SelectedMusicSource = MusicSources[0];
    }
}