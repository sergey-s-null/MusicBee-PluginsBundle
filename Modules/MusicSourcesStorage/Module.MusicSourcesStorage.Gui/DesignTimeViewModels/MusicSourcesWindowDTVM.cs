using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using Module.MusicSourcesStorage.Gui.DesignTimeViewModels.Nodes;
using Module.MusicSourcesStorage.Logic.Enums;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels;

public sealed class MusicSourcesWindowDTVM : IMusicSourcesWindowVM
{
    public IList<IMusicSourceVM> MusicSources { get; }
    public IMusicSourceVM? SelectedMusicSource { get; set; }

    public MusicSourcesWindowDTVM()
    {
        var hundred = new List<IConnectedNodeVM>();
        for (var i = 0; i < 100; i++)
        {
            hundred.Add(new ConnectedMusicFileDTVM($"Song{i}.mp3", MusicFileLocation.NotDownloaded, false));
        }

        MusicSources = new List<IMusicSourceVM>
        {
            new MusicSourceDTVM("First", MusicSourceType.Torrent, NodesHierarchyDTVM.ConnectedAllTypes),
            new MusicSourceDTVM("2nd", MusicSourceType.VkPostWithArchive, new NodesHierarchyDTVM<IConnectedNodeVM>(
                new IConnectedNodeVM[]
                {
                    new ConnectedMusicFileDTVM("Hello.mp3", MusicFileLocation.Library, true),
                    new ConnectedMusicFileDTVM("There.mp3", MusicFileLocation.NotDownloaded, true)
                }
            )),
            new MusicSourceDTVM("So Deep", MusicSourceType.VkPostWithArchive, new NodesHierarchyDTVM<IConnectedNodeVM>(
                new IConnectedNodeVM[]
                {
                    new ConnectedDirectoryDTVM("Right", new INodeVM[]
                    {
                        new ConnectedDirectoryDTVM("Right/Into", new INodeVM[]
                        {
                            new ConnectedDirectoryDTVM("Right/Into/The", new INodeVM[]
                            {
                                new ConnectedDirectoryDTVM("Right/Into/The/Abyss")
                            })
                        })
                    })
                }
            )),
            new MusicSourceDTVM("So BIG", MusicSourceType.Torrent, new NodesHierarchyDTVM<IConnectedNodeVM>(hundred))
        };

        for (var i = 0; i < 100; i++)
        {
            MusicSources.Add(new MusicSourceDTVM("-Inf", MusicSourceType.Torrent));
        }

        SelectedMusicSource = MusicSources[0];
    }
}