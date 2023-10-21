using System.Windows;
using System.Windows.Controls;

namespace Module.MusicSourcesStorage.Gui.Views.Components;

public partial class Directory : UserControl
{
    public static readonly DependencyProperty IsReadOnlyProperty = DependencyProperty.Register(
        nameof(IsReadOnly),
        typeof(bool),
        typeof(Directory),
        new PropertyMetadata(
            false,
            (obj, args) => OnIsReadOnlyChanged(
                (Directory)obj,
                (bool)args.OldValue,
                (bool)args.NewValue
            )
        )
    );

    public bool IsReadOnly
    {
        get => (bool)GetValue(IsReadOnlyProperty);
        set => SetValue(IsReadOnlyProperty, value);
    }

    public Directory()
    {
        InitializeComponent();

        UpdateContextMenuDisplayingState();
    }

    private void UpdateContextMenuDisplayingState()
    {
        ContextMenu = IsReadOnly
            ? null
            : new DirectoryContextMenu();
    }

    private static void OnIsReadOnlyChanged(Directory directory, bool oldValue, bool newValue)
    {
        if (oldValue == newValue)
        {
            return;
        }

        directory.UpdateContextMenuDisplayingState();
    }
}