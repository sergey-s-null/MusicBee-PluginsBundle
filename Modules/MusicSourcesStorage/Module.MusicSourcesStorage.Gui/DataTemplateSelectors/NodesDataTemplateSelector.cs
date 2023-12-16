using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using Module.MusicSourcesStorage.Gui.Views.Nodes;

namespace Module.MusicSourcesStorage.Gui.DataTemplateSelectors;

public sealed class NodesDataTemplateSelector : DataTemplateSelector
{
    public override DataTemplate SelectTemplate(object? viewModel, DependencyObject container)
    {
        if (viewModel is null)
        {
            return new DataTemplate();
            // todo throw later
            // throw new ArgumentNullException(
            //     nameof(viewModel),
            //     @"View model is null. Could provide appropriate data template."
            // );
        }

        var factory = new FrameworkElementFactory(GetViewType(viewModel));
        if (IsConnectedNode(viewModel))
        {
            factory.SetValue(HierarchyNodeBase.IsConnectedProperty, true);
        }

        return new HierarchicalDataTemplate
        {
            VisualTree = factory,
            ItemsSource = new Binding(nameof(INodeVM.ChildNodes))
        };
    }

    private static Type GetViewType(object viewModel)
    {
        return viewModel switch
        {
            IDirectoryVM or IConnectedDirectoryVM => typeof(Directory),
            IMusicFileVM or IConnectedMusicFileVM => typeof(MusicFile),
            IImageFileVM or IConnectedImageFileVM => typeof(ImageFile),
            IUnknownFileVM or IConnectedUnknownFileVM => typeof(UnknownFile),
            _ => throw new ArgumentOutOfRangeException(
                nameof(viewModel),
                viewModel,
                $@"Could not find appropriate view for view model {viewModel}."
            )
        };
    }

    private static bool IsConnectedNode(object viewModel)
    {
        return viewModel is IConnectedDirectoryVM
            or IConnectedMusicFileVM
            or IConnectedImageFileVM
            or IConnectedUnknownFileVM;
    }
}