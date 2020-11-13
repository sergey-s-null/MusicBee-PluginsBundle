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

namespace MusicBeePlugin.GUI
{
    /// <summary>
    /// Логика взаимодействия для SettingsDialog.xaml
    /// </summary>
    public partial class SettingsDialog : Window
    {
        private SettingsDialogViewModel _viewModel = new SettingsDialogViewModel();

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

        private void Dots_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new CommonOpenFileDialog())
            {
                dialog.IsFolderPicker = true;
                if (dialog.ShowDialog(this) == CommonFileDialogResult.Ok)
                    Console.WriteLine(dialog.FileName);
            }
        }
    }
}
