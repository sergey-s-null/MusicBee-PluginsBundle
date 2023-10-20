using System.Windows;
using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Gui.Views.Components;

namespace Module.MusicSourcesStorage.Gui;

public static class HierarchyNodesViewMapping
{
    public static readonly IDictionary<Type, Func<FrameworkElement>> Map =
        new Dictionary<Type, Func<FrameworkElement>>
        {
            [typeof(IDirectoryVM)] = () => new Directory(),
            [typeof(IMusicFileVM)] = () => new MusicFile(),
            [typeof(IImageFileVM)] = () => new ImageFile(),
            [typeof(IUnknownFileVM)] = () => new UnknownFile(),
        };
}