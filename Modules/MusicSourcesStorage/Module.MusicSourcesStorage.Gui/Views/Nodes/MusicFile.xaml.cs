using System.Windows;

namespace Module.MusicSourcesStorage.Gui.Views.Nodes;

public partial class MusicFile : HierarchyNodeBase
{
    public MusicFile()
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
        ContextMenu = new MusicFileContextMenu();
        StateIconControl.Content = CreateStateIcon();
    }

    protected override void OnBecameNotConnected()
    {
        ContextMenu = null;
        StateIconControl.Content = null;
    }

    private static FrameworkElement CreateStateIcon()
    {
        return new ProcessingIconWrapper
        {
            WrappedContent = new MusicFileStateIcon()
        };
    }
}