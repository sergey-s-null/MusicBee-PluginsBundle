using System.Collections.ObjectModel;
using System.Windows;
using Module.MusicSourcesStorage.Gui.Delegates;

namespace Module.MusicSourcesStorage.Gui.Views.Styles;

public sealed class ChildStylesCollection : Collection<Style>
{
    private readonly Action<Style>? _styleAdded;
    private readonly ItemReplacedCallback<Style>? _styleReplaced;
    private readonly Action<Style>? _styleRemoved;
    private readonly ItemsClearedCallback<Style>? _stylesCleared;

    public ChildStylesCollection(
        Action<Style>? styleAdded = null,
        ItemReplacedCallback<Style>? styleReplaced = null,
        Action<Style>? styleRemoved = null,
        ItemsClearedCallback<Style>? stylesCleared = null)
    {
        _styleAdded = styleAdded;
        _styleReplaced = styleReplaced;
        _styleRemoved = styleRemoved;
        _stylesCleared = stylesCleared;
    }

    protected override void InsertItem(int index, Style item)
    {
        base.InsertItem(index, item);
        _styleAdded?.Invoke(item);
    }

    protected override void SetItem(int index, Style item)
    {
        var prev = this[index];
        base.SetItem(index, item);
        _styleReplaced?.Invoke(prev, item);
    }

    protected override void RemoveItem(int index)
    {
        var item = this[index];
        base.RemoveItem(index);
        _styleRemoved?.Invoke(item);
    }

    protected override void ClearItems()
    {
        var items = this.ToList();
        base.ClearItems();
        _stylesCleared?.Invoke(items);
    }
}