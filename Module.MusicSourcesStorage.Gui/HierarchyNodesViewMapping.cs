using System.Windows;
using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Gui.Views.Components;

namespace Module.MusicSourcesStorage.Gui;

public static class HierarchyNodesViewMapping
{
    public static readonly IDictionary<Type, Func<FrameworkElement>> Map =
        new Dictionary<Type, Func<FrameworkElement>>
        {
            [typeof(IDirectoryVM)] = () => new Directory { IsReadOnly = true },
            [typeof(IMusicFileVM)] = () => new MusicFile { IsReadOnly = true },
            [typeof(IImageFileVM)] = () => new ImageFile { IsReadOnly = true },
            [typeof(IUnknownFileVM)] = () => new UnknownFile { IsReadOnly = true },
        };

    public static readonly IDictionary<Type, Func<FrameworkElement>> ConnectedMap =
        new Dictionary<Type, Func<FrameworkElement>>
        {
            [typeof(IConnectedDirectoryVM)] = () => new Directory(),
            [typeof(IConnectedMusicFileVM)] = () => new MusicFile(),
            [typeof(IConnectedImageFileVM)] = () => new ImageFile(),
            [typeof(IConnectedUnknownFileVM)] = () => new UnknownFile(),
        };
}