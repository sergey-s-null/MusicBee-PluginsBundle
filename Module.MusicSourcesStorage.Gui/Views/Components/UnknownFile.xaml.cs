namespace Module.MusicSourcesStorage.Gui.Views.Components;

public partial class UnknownFile : HierarchyNodeBase
{
    public UnknownFile()
    {
        InitializeComponent();

        UpdateDisplayingState();
    }

    protected override void OnIsReadOnlyChanged(bool oldValue, bool newValue)
    {
        if (oldValue == newValue)
        {
            return;
        }

        UpdateDisplayingState();
    }

    private void UpdateDisplayingState()
    {
        ContextMenu = IsReadOnly
            ? null
            : new UnknownFileContextMenu();
    }
}