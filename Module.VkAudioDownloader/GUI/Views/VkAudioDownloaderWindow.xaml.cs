using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Module.VkAudioDownloader.GUI.AbstractViewModels;

namespace Module.VkAudioDownloader.GUI.Views;

public sealed partial class VkAudioDownloaderWindow : Window
{
    private readonly IVkAudioDownloaderWindowVM _viewModel;

    public VkAudioDownloaderWindow(IVkAudioDownloaderWindowVM viewModel)
    {
        _viewModel = viewModel;

        InitializeComponent();
        DataContext = _viewModel;
    }

    protected override void OnContentRendered(EventArgs e)
    {
        _viewModel.Refresh.Execute(null);

        base.OnContentRendered(e);
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        if (_viewModel.IsDownloading)
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