using System;
using System.Windows;

namespace Module.InboxAdder.GUI.FileByIndexSelectDialog
{
    public partial class FileByIndexSelectDialog : Window
    {
        private readonly FileByIndexSelectDialogVM _viewModel;
        
        public FileByIndexSelectDialog(FileByIndexSelectDialogVM viewModel)
        {
            _viewModel = viewModel;
            
            InitializeComponent();
            DataContext = viewModel;
        }

        public bool ShowDialog(out string filePath)
        {
            if (ShowDialog() != true || _viewModel.FilePath is null)
            {
                filePath = string.Empty;
                return false;
            }
            
            filePath = _viewModel.FilePath;
            return true;
        }
        
        private void OnOkClick(object sender, RoutedEventArgs e)
        {
            if (_viewModel.FilePath is null)
            {
                MessageBox.Show("Nothing selected.", "Error");
                return;
            }
            
            DialogResult = true;
        }
        
        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}