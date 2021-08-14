using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ArtworksSearcher.GUI
{
    public partial class SearchWindow : Window
    {
        private readonly SearchWindowVM _viewModel;
        private bool _shown;

        private byte[] _imageData;
        public byte[] ImageData => _imageData;

        public SearchWindow(string artist, string title)
        {
            InitializeComponent();
            _viewModel = new SearchWindowVM() { Artist = artist, Title = title };
            _viewModel.OnClearResults += (_, _) => ResetScrollViewerOffset();
            DataContext = _viewModel;
        }

        private void ResetScrollViewerOffset()
        {
            ScrollViewer.ScrollToVerticalOffset(0);
        }

        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);

            if (_shown)
                return;
            _shown = true;

            if (_viewModel.SearchCmd.CanExecute(null))
                _viewModel.SearchCmd.Execute(null);
        }

        // direct events
        private void LeftPanelScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            var maxVerticalOffset = e.ExtentHeight - e.ViewportHeight;
            if (e.ExtentHeight < e.ViewportHeight || e.VerticalOffset / maxVerticalOffset > 0.99)
            {
                ICommand nextImageCmd = _viewModel.NextImageCmd;
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
                if (_viewModel.SearchCmd.CanExecute(null))
                    _viewModel.SearchCmd.Execute(null);
            }
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.TryGetImageData(out var imageData))
            {
                _imageData = imageData;
                DialogResult = true;
            }
        }
    }
}