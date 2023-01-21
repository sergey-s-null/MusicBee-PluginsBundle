using System.Windows;
using Test_Desktop.GUI.TestDialog;

namespace Test_Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public sealed partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var dialog = new TestDialog(new TestDialogVM());
            dialog.Show();
            // var wnd = new SettingsDialog();
            // wnd.ShowDialog();
        }
    }
}