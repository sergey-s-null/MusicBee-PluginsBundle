using System.Windows;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using Module.MusicSourcesStorage.Gui.Views.Nodes;

namespace Module.MusicSourcesStorage.Gui.ViewMappers;

public sealed class HierarchyNodesViewMapper : ViewMapperBase
{
    public static readonly IViewMapper Instance = new HierarchyNodesViewMapper();

    protected override IReadOnlyList<IReadOnlyDictionary<Type, Func<FrameworkElement>>> PrioritizedMaps { get; } = new[]
    {
        new Dictionary<Type, Func<FrameworkElement>>
        {
            [typeof(IConnectedDirectoryVM)] = () => new Directory { IsConnected = true },
            [typeof(IConnectedMusicFileVM)] = () => new MusicFile { IsConnected = true },
            [typeof(IConnectedImageFileVM)] = () => new ImageFile { IsConnected = true },
            [typeof(IConnectedUnknownFileVM)] = () => new UnknownFile { IsConnected = true },
        },
        new Dictionary<Type, Func<FrameworkElement>>
        {
            [typeof(IDirectoryVM)] = () => new Directory(),
            [typeof(IMusicFileVM)] = () => new MusicFile(),
            [typeof(IImageFileVM)] = () => new ImageFile(),
            [typeof(IUnknownFileVM)] = () => new UnknownFile(),
        },
    };

    private HierarchyNodesViewMapper()
    {
    }
}