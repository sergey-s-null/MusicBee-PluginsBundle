namespace Module.MusicSourcesStorage.Gui.Views.Nodes;

public partial class UnknownFile : HierarchyNodeBase
{
    public UnknownFile()
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
        ContextMenu = new UnknownFileContextMenu();
    }

    protected override void OnBecameNotConnected()
    {
        ContextMenu = null;
    }
}