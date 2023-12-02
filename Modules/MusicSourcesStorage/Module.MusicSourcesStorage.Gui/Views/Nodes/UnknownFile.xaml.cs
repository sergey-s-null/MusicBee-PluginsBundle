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
        StateIconControl.Content = new ProcessingIconWrapper
        {
            WrappedContent = new DownloadStateIcon()
        };
    }

    protected override void OnBecameNotConnected()
    {
        ContextMenu = null;
        StateIconControl.Content = null;
    }
}