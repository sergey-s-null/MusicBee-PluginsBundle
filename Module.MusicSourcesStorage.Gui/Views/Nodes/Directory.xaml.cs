﻿namespace Module.MusicSourcesStorage.Gui.Views.Nodes;

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
    }

    private void UpdateContextMenuDisplayingState()
    {
        ContextMenu = IsReadOnly
            ? null
            : new DirectoryContextMenu();
    }
}