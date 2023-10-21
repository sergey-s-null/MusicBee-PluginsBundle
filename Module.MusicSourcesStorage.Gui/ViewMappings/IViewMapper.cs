using System.Windows;

namespace Module.MusicSourcesStorage.Gui.ViewMappings;

public interface IViewMapper
{
    bool TryGetViewFactory(Type viewModelType, out Func<FrameworkElement> viewFactory);
}