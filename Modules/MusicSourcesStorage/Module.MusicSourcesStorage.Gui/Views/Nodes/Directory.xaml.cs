namespace Module.MusicSourcesStorage.Gui.Views.Nodes;

public partial class Directory : HierarchyNodeBase
{
    public Directory()
    {
        InitializeComponent();

        InitializeDisplayingState();
    }

    private void InitializeDisplayingState()
    {
        if (IsConnected)
        {
            OnBecameConnected();
        }
        else
        {
            OnBecameNotConnected();
        }
    }

    protected override void OnBecameConnected()
    {
        ContextMenu = new DirectoryContextMenu();
        StateIconControl.Content = new ProcessingIconWrapper
        {
            WrappedContent = new DirectoryStateIcon()
        };
    }

    protected override void OnBecameNotConnected()
    {
        ContextMenu = null;
        StateIconControl.Content = null;
    }
}