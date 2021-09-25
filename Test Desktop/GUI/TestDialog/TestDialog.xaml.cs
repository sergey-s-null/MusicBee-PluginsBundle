using System.Windows;
using Test_Desktop.GUI.SpecContextMenu;

namespace Test_Desktop.GUI.TestDialog
{
    public partial class TestDialog : Window
    {
        public TestDialog(TestDialogVM vm)
        {
            InitializeComponent();
            DataContext = vm;

            var contextMenu = ResourceMenuGetter.Get();
            contextMenu.IsOpen = true;
        }

    }
}