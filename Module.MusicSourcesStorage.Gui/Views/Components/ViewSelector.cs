using System.Windows;
using System.Windows.Controls;
using Module.MusicSourcesStorage.Gui.Exceptions;
using Module.MusicSourcesStorage.Gui.ViewMappings;

namespace Module.MusicSourcesStorage.Gui.Views.Components;

public class ViewSelector : ContentControl
{
    public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
        nameof(ViewModel),
        typeof(object),
        typeof(ViewSelector),
        new FrameworkPropertyMetadata(
            null,
            FrameworkPropertyMetadataOptions.AffectsRender,
            (obj, _) =>
            {
                var viewSelector = (ViewSelector)obj;
                viewSelector.OnViewModelOrMapperChanged();
            }
        )
    );

    public static readonly DependencyProperty ViewMapperProperty = DependencyProperty.Register(
        nameof(ViewMapper),
        typeof(IViewMapper),
        typeof(ViewSelector),
        new PropertyMetadata(
            null,
            (obj, _) =>
            {
                var viewSelector = (ViewSelector)obj;
                viewSelector.OnViewModelOrMapperChanged();
            }
        )
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

    public IViewMapper? ViewMapper
    {
        get => (IViewMapper?)GetValue(ViewMapperProperty);
        set => SetValue(ViewMapperProperty, value);
    }

    public bool ThrowExceptionOnFallback
    {
        get => (bool)GetValue(ThrowExceptionOnFallbackProperty);
        set => SetValue(ThrowExceptionOnFallbackProperty, value);
    }

    private void OnViewModelOrMapperChanged()
    {
        if (ViewModel is null || ViewMapper is null)
        {
            Content = null;
            return;
        }

        if (!ViewMapper.TryGetViewFactory(ViewModel.GetType(), out var viewFactory))
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
}