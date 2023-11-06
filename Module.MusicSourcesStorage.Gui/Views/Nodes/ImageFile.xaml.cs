namespace Module.MusicSourcesStorage.Gui.Views.Nodes;

public partial class ImageFile : HierarchyNodeBase
{
    public ImageFile()
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
        ContextMenu = new ImageFileContextMenu();
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