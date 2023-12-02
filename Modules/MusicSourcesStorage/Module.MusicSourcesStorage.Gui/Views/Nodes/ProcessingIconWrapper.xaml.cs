using System.Windows;
using System.Windows.Controls;

namespace Module.MusicSourcesStorage.Gui.Views.Nodes;

public partial class ProcessingIconWrapper : ContentControl
{
    public static readonly DependencyProperty WrappedContentProperty = DependencyProperty.Register(
        nameof(WrappedContent),
        typeof(FrameworkElement),
        typeof(ProcessingIconWrapper),
        new FrameworkPropertyMetadata(
            null
        )
    );

    public FrameworkElement? WrappedContent
    {
        get => (FrameworkElement)GetValue(WrappedContentProperty);
        set => SetValue(WrappedContentProperty, value);
    }

    public ProcessingIconWrapper()
    {
        InitializeComponent();
    }
}