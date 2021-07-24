using System.Windows;

namespace VkMusicDownloader.GUI.SettingsDialog
{
    /// <summary>
    /// Логика взаимодействия для SettingsDialog.xaml
    /// </summary>
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
            if (_viewModel.SaveChanges())
                DialogResult = true;
        }
    }
}
