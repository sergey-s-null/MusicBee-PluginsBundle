namespace Module.MusicSourcesStorage.Gui.Views.Components;

public partial class Directory : HierarchyNodeBase
{
    public Directory()
    {
        InitializeComponent();

        UpdateContextMenuDisplayingState();
    }

    protected override void OnIsReadOnlyChanged(bool oldValue, bool newValue)
    {
        if (oldValue == newValue)
        {
            return;
        }

        UpdateContextMenuDisplayingState();
        throw new NotImplementedException();
    }

    private void UpdateContextMenuDisplayingState()
    {
        ContextMenu = IsReadOnly
            ? null
            : new DirectoryContextMenu();
    }
}