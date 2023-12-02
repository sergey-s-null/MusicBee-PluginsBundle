using System.Windows;

namespace Module.MusicSourcesStorage.Gui.ViewMappers;

public interface IViewMapper
{
    bool TryGetViewFactory(Type viewModelType, out Func<FrameworkElement> viewFactory);
}