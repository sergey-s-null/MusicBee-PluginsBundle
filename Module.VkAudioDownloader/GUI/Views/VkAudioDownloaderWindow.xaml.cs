using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Module.VkAudioDownloader.GUI.AbstractViewModels;
using Module.VkAudioDownloader.GUI.Comparers;
using Root.Collections;

namespace Module.VkAudioDownloader.GUI.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class VkAudioDownloaderWindow : Window
    {
        public IVkAudioDownloaderWindowVM ViewModel { get; }

        public VkAudioDownloaderWindow(IVkAudioDownloaderWindowVM viewModel)
        {
            InitializeComponent();

            ViewModel = viewModel;

            DataContext = ViewModel;
        }

        private void VkAudioDownloaderWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            var sortedAudiosSource = (CollectionViewSource) Resources["SortedAudios"];
            var sortedAudiosView = (ListCollectionView) sortedAudiosSource.View;
            sortedAudiosView.CustomSort = new ReverseComparer(new AudioVMComparer());
        }

        protected override void OnContentRendered(EventArgs e)
        {
            ICommand cmd = ViewModel.RefreshCmd;
            cmd.Execute(null);
            ViewModel.RefreshCmd.Execute(null);

            base.OnContentRendered(e);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (ViewModel.IsDownloading)
            {
                if (MessageBox.Show("Downloading in process. Are you sure to close window?", "!!!",
                        MessageBoxButton.YesNo) == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
            }

            base.OnClosing(e);
        }
    }
}