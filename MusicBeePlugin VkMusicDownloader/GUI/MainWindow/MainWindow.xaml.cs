using MusicBeePlugin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VkMusicDownloader.GUI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IMainWindowVM _viewModel;

        public VkNet.VkApi VkApi
        {
            get => _viewModel.VkApi;
            set => _viewModel.VkApi = value;
        }

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainWindowVM();
            DataContext = _viewModel;
        }

        public MainWindow(IMainWindowVM vm)
        {
            InitializeComponent();
            _viewModel = vm;
            DataContext = _viewModel;
        }

        protected override void OnContentRendered(EventArgs e)
        {
            if (!_viewModel.IsRefreshing)
                _viewModel.RefreshCmd.Execute(null);
            base.OnContentRendered(e);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (_viewModel.IsApplying)
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
