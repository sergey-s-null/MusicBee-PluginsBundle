using System.Windows;
using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Gui.Views.Components;

namespace Module.MusicSourcesStorage.Gui.ViewMappers;

public sealed class HierarchyNodesViewMapper : ViewMapperBase
{
    public static readonly IViewMapper Instance = new HierarchyNodesViewMapper();

    protected override IReadOnlyList<IReadOnlyDictionary<Type, Func<FrameworkElement>>> PrioritizedMaps { get; } = new[]
    {
        new Dictionary<Type, Func<FrameworkElement>>
        {
            [typeof(IConnectedDirectoryVM)] = () => new Directory(),
            [typeof(IConnectedMusicFileVM)] = () => new MusicFile(),
            [typeof(IConnectedImageFileVM)] = () => new ImageFile(),
            [typeof(IConnectedUnknownFileVM)] = () => new UnknownFile(),
        },
        new Dictionary<Type, Func<FrameworkElement>>
        {
            [typeof(IDirectoryVM)] = () => new Directory { IsReadOnly = true },
            [typeof(IMusicFileVM)] = () => new MusicFile { IsReadOnly = true },
            [typeof(IImageFileVM)] = () => new ImageFile { IsReadOnly = true },
            [typeof(IUnknownFileVM)] = () => new UnknownFile { IsReadOnly = true },
        },
    };

    private HierarchyNodesViewMapper()
    {
    }
}