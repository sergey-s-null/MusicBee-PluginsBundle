using System.Windows;

namespace Module.MusicSourcesStorage.Gui.Views.Components;

// Костыль - внутри HierarchicalDataTemplate невозможно выставить свойтво из статического контекст.
// Не работает
//    x:Static ...
//    Binging ..., Source={x:Static ...}
//    Binging ..., Source={StaticResource ...}
public sealed class HierarchyNodesViewSelector : ViewSelector
{
    public static readonly DependencyProperty UseConnectedViewMappingProperty = DependencyProperty.Register(
        nameof(UseConnectedViewMapping),
        typeof(bool),
        typeof(HierarchyNodesViewSelector),
        new PropertyMetadata(
            false,
            (obj, _) =>
            {
                var selector = (HierarchyNodesViewSelector)obj;
                selector.UpdateSelectionTable();
            }
        )
    );

    public bool UseConnectedViewMapping
    {
        get => (bool)GetValue(UseConnectedViewMappingProperty);
        set => SetValue(UseConnectedViewMappingProperty, value);
    }

    public HierarchyNodesViewSelector()
    {
        UpdateSelectionTable();
    }

    private void UpdateSelectionTable()
    {
        SelectionTable = UseConnectedViewMapping
            ? HierarchyNodesViewMapping.ConnectedMap
            : HierarchyNodesViewMapping.Map;
    }
}