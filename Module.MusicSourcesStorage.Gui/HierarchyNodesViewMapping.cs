using System.Windows;
using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Gui.Views.Components;

namespace Module.MusicSourcesStorage.Gui;

public static class HierarchyNodesViewMapping
{
    // todo make 2 different maps - for read only and for editable
    public static readonly IDictionary<Type, Func<FrameworkElement>> Map =
        new Dictionary<Type, Func<FrameworkElement>>
        {
            [typeof(IReadOnlyDirectoryVM)] = () => new Directory(),
            [typeof(IMusicFileVM)] = () => new MusicFile(),
            [typeof(IReadOnlyImageFileVM)] = () => new ImageFile(),
            [typeof(IUnknownFileVM)] = () => new UnknownFile(),
        };
}