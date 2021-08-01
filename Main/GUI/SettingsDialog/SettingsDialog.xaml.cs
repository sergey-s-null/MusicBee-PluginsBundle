using System.Windows;

namespace MusicBeePlugin.GUI.SettingsDialog
{
    public partial class SettingsDialog : Window
    {
        public SettingsDialog(SettingsDialogVM viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}