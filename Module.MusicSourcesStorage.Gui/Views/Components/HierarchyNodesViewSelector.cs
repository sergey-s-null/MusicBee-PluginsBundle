namespace Module.MusicSourcesStorage.Gui.Views.Components;

// Костыль - внутри HierarchicalDataTemplate невозможно выставить свойтво из статического контекста.
// Не работает
//    x:Static ...
//    Binging ..., Source={x:Static ...}
//    Binging ..., Source={StaticResource ...}
public sealed class HierarchyNodesViewSelector : ViewSelector
{
    public HierarchyNodesViewSelector()
    {
        ViewMapper = HierarchyNodesViewMapper.Instance;
    }
}