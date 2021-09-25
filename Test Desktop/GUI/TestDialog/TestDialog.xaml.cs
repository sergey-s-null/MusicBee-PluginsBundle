using System.Windows;

namespace Test_Desktop.GUI.TestDialog
{
    public partial class TestDialog : Window
    {
        public TestDialog(TestDialogVM vm)
        {
            InitializeComponent();
            DataContext = vm;
            vm.Button = Button;
            
            vm.OpenMenu();
        }

        private void ContextMenu_OnClosed(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}