using System.Windows;

namespace Module.MusicSourcesStorage.Gui.ViewMappers;

// todo use DataTemplateSelector instead
public interface IViewMapper
{
    bool TryGetViewFactory(Type viewModelType, out Func<FrameworkElement> viewFactory);
}