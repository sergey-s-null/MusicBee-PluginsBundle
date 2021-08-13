using System;
using System.ComponentModel;
using System.Windows;
using Module.VkMusicDownloader.Helpers;
using Module.VkMusicDownloader.Settings;
using VkNet.Abstractions;

namespace Module.VkMusicDownloader.GUI.MusicDownloaderWindow
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MusicDownloaderWindow : Window
    {
        private readonly MusicDownloaderWindowVM _viewModel;

        private readonly IVkApi _vkApi;
        private readonly IMusicDownloaderSettings _settings;
        
        public MusicDownloaderWindow(MusicDownloaderWindowVM viewModel,
            IVkApi vkApi,
            IMusicDownloaderSettings settings)
        {
            InitializeComponent();
            
            _viewModel = viewModel;
            _vkApi = vkApi;
            _settings = settings;
            
            DataContext = _viewModel;
        }

        public new void ShowDialog()
        {
            if (!_vkApi.IsAuthorized)
            {
                var token = _settings.AccessToken;
                if (!_vkApi.TryAuth(token))
                {
                    if (_vkApi.TryAuth(TryInputAuthData, TryInputCode))
                    {
                        _settings.AccessToken = _vkApi.Token;
                        _settings.Save();
                    }
                    else
                    {
                        MessageBox.Show("Auth error.");
                        return;
                    }
                }
            }

            base.ShowDialog();
        }
        
        // TODO move out
        private bool TryInputAuthData(out string login, out string password)
        {
            var dialog = new AuthDialog.AuthDialog();
            
            return dialog.ShowDialog(out login, out password);
        }
        
        // TODO move out
        private bool TryInputCode(out string code)
        {
            var dialog = new InputDialog.InputDialog();

            return dialog.ShowDialog("Enter code:", out code);
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
