using System.Windows.Controls;
using Plugin.Main.GUI.ViewModels;

namespace Plugin.Main.GUI.Views;

public partial class InboxRelocateContextMenu : ContextMenu
{
    public InboxRelocateContextMenu(InboxRelocateContextMenuVM viewModel)
    {
        InitializeComponent();

        DataContext = viewModel;
    }
}