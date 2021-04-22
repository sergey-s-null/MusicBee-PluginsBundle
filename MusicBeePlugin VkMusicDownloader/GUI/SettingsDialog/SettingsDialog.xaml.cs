using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace VkMusicDownloader.GUI
{
    /// <summary>
    /// Логика взаимодействия для SettingsDialog.xaml
    /// </summary>
    public partial class SettingsDialog : Window
    {
        private SettingsDialogVM _viewModel = new SettingsDialogVM();

        public SettingsDialog()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SaveChanges())
                DialogResult = true;
        }
    }
}
