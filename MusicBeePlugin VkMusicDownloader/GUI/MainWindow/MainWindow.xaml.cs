using MusicBeePlugin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using VkMusicDownloader.VkApi;

namespace VkMusicDownloader.GUI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowVM _viewModel;

        public MainWindow(VkAudioApi vkApi)
        {
            InitializeComponent();
            _viewModel = new MainWindowVM(vkApi);
            DataContext = _viewModel;
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
