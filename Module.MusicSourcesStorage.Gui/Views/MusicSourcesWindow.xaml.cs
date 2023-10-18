using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Module.MusicSourcesStorage.Gui.AbstractViewModels;

namespace Module.MusicSourcesStorage.Gui.Views;

public partial class MusicSourcesWindow : Window
{
    public MusicSourcesWindow(IMusicSourcesWindowVM viewModel)
    {
        InitializeComponent();

        DataContext = viewModel;
    }

    private void OnPreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
    {
        var source = e.OriginalSource as DependencyObject;
        while (source is not null && source is not TreeViewItem)
        {
            source = VisualTreeHelper.GetParent(source);
        }

        if (source is TreeViewItem treeViewItem)
        {
            treeViewItem.Focus();
            e.Handled = true;
        }
    }
}