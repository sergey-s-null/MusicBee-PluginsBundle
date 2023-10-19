using System.Windows;
using Module.MusicSourcesStorage.Gui.AbstractViewModels;

namespace Module.MusicSourcesStorage.Gui.Views;

public partial class SourceAddingWizard : Window
{
    public SourceAddingWizard(ISourceAddingWizardVM viewModel)
    {
        InitializeComponent();

        DataContext = viewModel;
    }
}