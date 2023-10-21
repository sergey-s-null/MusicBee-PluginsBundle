using System.Windows;
using System.Windows.Controls;

namespace Module.MusicSourcesStorage.Gui.Views.Components;

public abstract class HierarchyNodeBase : ContentControl
{
    public static readonly DependencyProperty IsReadOnlyProperty = DependencyProperty.Register(
        nameof(IsReadOnly),
        typeof(bool),
        typeof(HierarchyNodeBase),
        new PropertyMetadata(
            false,
            (obj, args) =>
            {
                var node = ((HierarchyNodeBase)obj);
                node.OnIsReadOnlyChanged((bool)args.OldValue, (bool)args.NewValue);
            }
        )
    );

    public bool IsReadOnly
    {
        get => (bool)GetValue(IsReadOnlyProperty);
        set => SetValue(IsReadOnlyProperty, value);
    }

    protected abstract void OnIsReadOnlyChanged(bool oldValue, bool newValue);
}