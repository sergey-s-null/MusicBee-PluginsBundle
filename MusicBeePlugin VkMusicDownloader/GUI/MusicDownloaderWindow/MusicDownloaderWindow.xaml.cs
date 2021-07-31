using System;
using System.ComponentModel;
using System.Windows;

namespace VkMusicDownloader.GUI.MusicDownloaderWindow
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MusicDownloaderWindow : Window
    {
        private readonly MusicDownloaderWindowVM _viewModel;
        
        public MusicDownloaderWindow(MusicDownloaderWindowVM viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
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
