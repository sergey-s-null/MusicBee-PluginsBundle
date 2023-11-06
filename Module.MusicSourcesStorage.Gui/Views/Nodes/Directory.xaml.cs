namespace Module.MusicSourcesStorage.Gui.Views.Nodes;

public partial class Directory : HierarchyNodeBase
{
    public Directory()
    {
        InitializeComponent();

        UpdateComponentsDisplayingState();
    }

    // todo rename to "Connected"
    protected override void OnIsReadOnlyChanged(bool oldValue, bool newValue)
    {
        if (oldValue == newValue)
        {
            return;
        }

        UpdateComponentsDisplayingState();
    }

    // todo move to base class
    private void UpdateComponentsDisplayingState()
    {
        if (IsReadOnly)
        {
            ConfigureReadOnlyState();
        }
        else
        {
            ConfigureEditableState();
        }
    }

    private void ConfigureReadOnlyState()
    {
        ContextMenu = null;
        StateIconControl.Content = null;
    }

    private void ConfigureEditableState()
    {
        ContextMenu = new DirectoryContextMenu();
        StateIconControl.Content = new ProcessingIconWrapper
        {
            WrappedContent = new DirectoryStateIcon()
        };
    }
}