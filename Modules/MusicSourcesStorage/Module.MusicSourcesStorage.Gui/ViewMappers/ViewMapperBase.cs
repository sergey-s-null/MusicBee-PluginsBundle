using System.Windows;

namespace Module.MusicSourcesStorage.Gui.ViewMappers;

// todo use DataTemplateSelector instead
public abstract class ViewMapperBase : IViewMapper
{
    protected abstract IReadOnlyList<IReadOnlyDictionary<Type, Func<FrameworkElement>>> PrioritizedMaps { get; }

    public bool TryGetViewFactory(Type viewModelType, out Func<FrameworkElement> viewFactory)
    {
        var implementedInterfaces = viewModelType.GetInterfaces();

        foreach (var map in PrioritizedMaps)
        {
            if (map.TryGetValue(viewModelType, out viewFactory))
            {
                return true;
            }

            foreach (var implementedInterface in implementedInterfaces)
            {
                if (map.TryGetValue(implementedInterface, out viewFactory))
                {
                    return true;
                }
            }
        }

        viewFactory = null!;
        return false;
    }
}