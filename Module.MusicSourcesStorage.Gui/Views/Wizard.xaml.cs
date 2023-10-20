using System.Windows;
using Module.MusicSourcesStorage.Gui.AbstractViewModels;

namespace Module.MusicSourcesStorage.Gui.Views;

public partial class Wizard : Window
{
    public Wizard(IWizardVM viewModel)
    {
        InitializeComponent();

        DataContext = viewModel;
    }
}