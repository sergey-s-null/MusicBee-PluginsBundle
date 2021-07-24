using System;
using System.ComponentModel;
using System.Windows;

namespace VkMusicDownloader.GUI.MainWindow
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainWindowVM _viewModel;
        
        public MainWindow(MainWindowVM mainWindowVM)
        {
            InitializeComponent();
            _viewModel = mainWindowVM;
            DataContext = _viewModel;
        }

        protected override void OnContentRendered(EventArgs e)
        {
            if (!_viewModel.AddingVkVM.IsRefreshing)
                _viewModel.AddingVkVM.RefreshCmd.Execute(null);
            base.OnContentRendered(e);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (_viewModel.AddingVkVM.IsApplying)
            {
                if (MessageBox.Show("Downloading in process. Are you sure to close window?", "!!!", MessageBoxButton.YesNo) == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
            }
            
            base.OnClosing(e);
        }
    }
}
