using System.Windows;

namespace Module.MusicSourcesStorage.Gui.Views.Components;

public partial class MusicFile : HierarchyNodeBase
{
    public MusicFile()
    {
        InitializeComponent();

        UpdateComponentsDisplayingState();
    }

    protected override void OnIsReadOnlyChanged(bool oldValue, bool newValue)
    {
        if (oldValue == newValue)
        {
            return;
        }

        UpdateComponentsDisplayingState();
    }

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
        ContextMenu = new MusicFileContextMenu();
        StateIconControl.Content = CreateStateIcon();
    }

    private static MusicFileStateIcon CreateStateIcon()
    {
        return new MusicFileStateIcon
        {
            Margin = new Thickness(6, 0, 4, 0)
        };
    }
}