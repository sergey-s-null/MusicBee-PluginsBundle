using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Module.MusicSourcesStorage.Gui.Views.Components;

public partial class NodesHierarchy : UserControl
{
    public static readonly DependencyProperty IsConnectedToStorageProperty = DependencyProperty.Register(
        nameof(IsConnectedToStorage),
        typeof(bool),
        typeof(NodesHierarchy),
        new PropertyMetadata(false)
    );

    public bool IsConnectedToStorage
    {
        get => (bool)GetValue(IsConnectedToStorageProperty);
        set => SetValue(IsConnectedToStorageProperty, value);
    }

    public NodesHierarchy()
    {
        InitializeComponent();
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