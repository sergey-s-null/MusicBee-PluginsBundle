using System.ComponentModel;
using System.Windows;
using Module.MusicSourcesStorage.Gui.AbstractViewModels;

namespace Module.MusicSourcesStorage.Gui.Views;

public partial class Wizard : Window
{
    private readonly IWizardVM _viewModel;

    public Wizard(Func<Window, IWizardVM> viewModelFactory)
    {
        InitializeComponent();

        _viewModel = viewModelFactory(this);
        DataContext = _viewModel;
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        if (_viewModel.CurrentStep.CanSafelyCloseWizard)
        {
            base.OnClosing(e);
            return;
        }

        var result = MessageBox.Show(
            this,
            "Close wizard?",
            "(ಠ_ಠ)",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question
        );

        if (result != MessageBoxResult.Yes)
        {
            e.Cancel = true;
        }
    }
}