using System.Windows;

namespace Module.MusicSourcesStorage.Gui;

public static class ViewModelToViewMapping
{
    public static readonly IDictionary<Type, Func<FrameworkElement>> Map =
        new Dictionary<Type, Func<FrameworkElement>>
        {
            // [typeof(IFirstStepVM)] = () => new FirstStep(),
        };
}