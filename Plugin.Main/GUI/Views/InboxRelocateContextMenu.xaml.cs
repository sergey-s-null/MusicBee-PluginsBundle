using System.Windows.Controls;
using MusicBeePlugin.GUI.ViewModels;

namespace MusicBeePlugin.GUI.Views;

public partial class InboxRelocateContextMenu : ContextMenu
{
    public InboxRelocateContextMenu(InboxRelocateContextMenuVM viewModel)
    {
        InitializeComponent();

        DataContext = viewModel;
    }
}