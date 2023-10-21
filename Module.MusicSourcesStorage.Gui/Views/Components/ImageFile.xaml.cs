namespace Module.MusicSourcesStorage.Gui.Views.Components;

public partial class ImageFile : HierarchyNodeBase
{
    public ImageFile()
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
    }

    private void UpdateContextMenuDisplayingState()
    {
        ContextMenu = IsReadOnly
            ? null
            : new ImageFileContextMenu();
    }
}