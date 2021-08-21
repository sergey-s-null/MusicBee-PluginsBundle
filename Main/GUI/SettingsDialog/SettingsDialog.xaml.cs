using System.Windows;

namespace MusicBeePlugin.GUI.SettingsDialog
{
    public partial class SettingsDialog : Window
    {
        private readonly SettingsDialogVM _viewModel;
        
        public SettingsDialog(SettingsDialogVM viewModel)
        {
            InitializeComponent();

            _viewModel = viewModel;
            DataContext = viewModel;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.Save())
            {
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Error save settings.", "Error");
            }
        }
    }
}