using System.Windows;
using System.Windows.Controls;

namespace Module.MusicSourcesStorage.Gui.Views.Components;

public class ViewSelector : ContentControl
{
    public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
        nameof(ViewModel),
        typeof(object),
        typeof(ViewSelector),
        new FrameworkPropertyMetadata(null, OnViewModelChanged)
    );

    public static readonly DependencyProperty SelectionTableProperty = DependencyProperty.Register(
        nameof(SelectionTable),
        typeof(IDictionary<Type, Func<FrameworkElement>>),
        typeof(ViewSelector),
        new FrameworkPropertyMetadata(new Dictionary<Type, Func<FrameworkElement>>())
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

    private static void OnViewModelChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
    {
        var viewSelector = (ViewSelector)dependencyObject;

        var viewModel = viewSelector.ViewModel;
        if (viewModel is null)
        {
            viewSelector.Content = null;
            return;
        }

        var selectionTable = viewSelector.SelectionTable;
        var type = viewModel.GetType();

        if (!TryFindAppropriateViewFactory(selectionTable, type, out var viewFactory))
        {
            viewSelector.Content = null;
            return;
        }

        var view = viewFactory();
        view.DataContext = viewModel;
        viewSelector.Content = view;
    }

    private static bool TryFindAppropriateViewFactory(
        IDictionary<Type, Func<FrameworkElement>> selectionTable,
        Type viewModelType,
        out Func<FrameworkElement> viewFactory)
    {
        if (selectionTable.TryGetValue(viewModelType, out viewFactory))
        {
            return true;
        }

        var implementedInterfaces = viewModelType.GetInterfaces();
        foreach (var implementedInterface in implementedInterfaces)
        {
            if (selectionTable.TryGetValue(implementedInterface, out viewFactory))
            {
                return true;
            }
        }

        return false;
    }
}