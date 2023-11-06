using System.Windows;
using System.Windows.Controls;

namespace Module.MusicSourcesStorage.Gui.Views.Nodes;

public abstract class HierarchyNodeBase : ContentControl
{
    public static readonly DependencyProperty IsConnectedProperty = DependencyProperty.Register(
        nameof(IsConnected),
        typeof(bool),
        typeof(HierarchyNodeBase),
        new PropertyMetadata(
            false,
            (obj, args) =>
            {
                var (oldIsConnected, isConnected) = ((bool)args.OldValue, (bool)args.NewValue);
                if (oldIsConnected == isConnected)
                {
                    return;
                }

                var node = (HierarchyNodeBase)obj;
                if (isConnected)
                {
                    node.OnBecameConnected();
                }
                else
                {
                    node.OnBecameNotConnected();
                }
            }
        )
    );

    public bool IsConnected
    {
        get => (bool)GetValue(IsConnectedProperty);
        set => SetValue(IsConnectedProperty, value);
    }

    protected abstract void OnBecameConnected();

    protected abstract void OnBecameNotConnected();
}