using System.Windows;
using Module.VkAudioDownloader.GUI.AbstractViewModels;

namespace Module.VkAudioDownloader.GUI.Views;

/// <summary>
/// Логика взаимодействия для AuthDialog.xaml
/// </summary>
public sealed partial class AuthorizationWindow : Window
{
    private readonly IAuthorizationWindowVM _viewModel;

    public AuthorizationWindow(IAuthorizationWindowVM viewModel)
    {
        _viewModel = viewModel;

        InitializeComponent();

        DataContext = viewModel;
        viewModel.ClosingRequested += (_, _) => DialogResult = true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>Authorization result</returns>
    public new bool ShowDialog()
    {
        base.ShowDialog();

        return _viewModel.AuthorizationResult;
    }
}