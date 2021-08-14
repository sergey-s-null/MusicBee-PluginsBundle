using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Module.ArtworksSearcher.GUI.SearchWindow
{
    public partial class SearchWindow : Window
    {
        public SearchWindowVM ViewModel { get; }

        private bool _shown;

        private byte[] _imageData;

        public SearchWindow(SearchWindowVM viewModel)
        {
            InitializeComponent();
            
            ViewModel = viewModel;
            
            ViewModel.OnClearResults += (_, _) => ResetScrollViewerOffset();
            DataContext = ViewModel;
        }

        private void ResetScrollViewerOffset()
        {
            ScrollViewer.ScrollToVerticalOffset(0);
        }

        public bool ShowDialog(string artist, string title, out byte[] imageData)
        {
            ViewModel.Artist = artist;
            ViewModel.Title = title;
            
            if (ShowDialog() is true && _imageData is not null)
            {
                imageData = _imageData;
                return true;
            }

            imageData = null;
            return false;
        }
        
        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);

            if (_shown)
                return;
            _shown = true;

            if (ViewModel.SearchCmd.CanExecute(null))
                ViewModel.SearchCmd.Execute(null);
        }

        // direct events
        private void LeftPanelScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            var maxVerticalOffset = e.ExtentHeight - e.ViewportHeight;
            if (e.ExtentHeight < e.ViewportHeight || e.VerticalOffset / maxVerticalOffset > 0.99)
            {
                ICommand nextImageCmd = ViewModel.NextImageCmd;
                if (nextImageCmd.CanExecute(null))
                {
                    nextImageCmd.Execute(null);
                }
            }
        }

        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (ViewModel.SearchCmd.CanExecute(null))
                    ViewModel.SearchCmd.Execute(null);
            }
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.TryGetImageData(out var imageData))
            {
                _imageData = imageData;
                DialogResult = true;
            }
        }
    }
}