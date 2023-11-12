using System.Windows;

namespace Module.MusicSourcesStorage.Gui.Views.Styles;

public sealed class MergedStyle : Style
{
    public ChildStylesCollection ChildStyles { get; }

    public MergedStyle()
    {
        ChildStyles = new ChildStylesCollection(
            OnChildStyleAdded,
            OnChildStyleReplaced,
            OnChildStyleRemoved,
            OnChildStylesCleared
        );
    }

    private void OnChildStyleAdded(Style style)
    {
        CheckTargetTypesCompatibility(style);

        foreach (var key in style.Resources.Keys)
        {
            // Resources.mer
            Resources.Add(key, style.Resources[key]);
        }

        foreach (var setter in style.Setters)
        {
            Setters.Add(setter);
        }

        foreach (var trigger in style.Triggers)
        {
            Triggers.Add(trigger);
        }
    }

    private void OnChildStyleReplaced(Style removedStyle, Style addedStyle)
    {
        throw new NotImplementedException();
    }

    private void OnChildStyleRemoved(Style style)
    {
        throw new NotImplementedException();
    }

    private void OnChildStylesCleared(IReadOnlyList<Style> styles)
    {
        throw new NotImplementedException();
    }

    private void CheckTargetTypesCompatibility(Style style)
    {
        if (!style.TargetType.IsAssignableFrom(TargetType))
        {
            throw new InvalidOperationException(
                $"Could not merge style with target type {style.TargetType} to style with target type {TargetType}."
            );
        }
    }
}