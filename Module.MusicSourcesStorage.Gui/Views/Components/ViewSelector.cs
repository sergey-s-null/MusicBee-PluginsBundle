using System.Windows;
using System.Windows.Controls;
using Module.MusicSourcesStorage.Gui.Exceptions;

namespace Module.MusicSourcesStorage.Gui.Views.Components;

public class ViewSelector : ContentControl
{
    public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
        nameof(ViewModel),
        typeof(object),
        typeof(ViewSelector),
        new FrameworkPropertyMetadata(
            null,
            (obj, _) =>
            {
                var viewSelector = (ViewSelector)obj;
                viewSelector.OnViewModelChanged();
            }
        )
    );

    public static readonly DependencyProperty SelectionTableProperty = DependencyProperty.Register(
        nameof(SelectionTable),
        typeof(IDictionary<Type, Func<FrameworkElement>>),
        typeof(ViewSelector),
        new FrameworkPropertyMetadata(new Dictionary<Type, Func<FrameworkElement>>())
    );

    public static readonly DependencyProperty ThrowExceptionOnFallbackProperty = DependencyProperty.Register(
        nameof(ThrowExceptionOnFallback),
        typeof(bool),
        typeof(ViewSelector),
        new PropertyMetadata(true)
    );

    public object? ViewModel
    {
        get => GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }

    public IDictionary<Type, Func<FrameworkElement>> SelectionTable
    {
        get => (IDictionary<Type, Func<FrameworkElement>>)GetValue(SelectionTableProperty);
        set => SetValue(SelectionTableProperty, value);
    }

    public bool ThrowExceptionOnFallback
    {
        get => (bool)GetValue(ThrowExceptionOnFallbackProperty);
        set => SetValue(ThrowExceptionOnFallbackProperty, value);
    }

    private void OnViewModelChanged()
    {
        if (ViewModel is null)
        {
            Content = null;
            return;
        }

        if (!TryFindAppropriateViewFactory(out var viewFactory))
        {
            if (ThrowExceptionOnFallback)
            {
                throw new ViewSelectionException(
                    $"Could not find appropriate view factory for view model: {ViewModel}.");
            }

            Content = null;
            return;
        }

        var view = viewFactory();
        view.DataContext = ViewModel;
        Content = view;
    }

    private bool TryFindAppropriateViewFactory(out Func<FrameworkElement> viewFactory)
    {
        var viewModelType = ViewModel!.GetType();

        if (SelectionTable.TryGetValue(viewModelType, out viewFactory))
        {
            return true;
        }

        var implementedInterfaces = viewModelType.GetInterfaces();
        foreach (var implementedInterface in implementedInterfaces)
        {
            if (SelectionTable.TryGetValue(implementedInterface, out viewFactory))
            {
                return true;
            }
        }

        return false;
    }
}