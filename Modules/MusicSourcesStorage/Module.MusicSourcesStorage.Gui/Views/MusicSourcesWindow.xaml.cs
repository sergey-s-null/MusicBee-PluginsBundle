using System.Windows;
using Module.MusicSourcesStorage.Gui.AbstractViewModels;

namespace Module.MusicSourcesStorage.Gui.Views;

public partial class MusicSourcesWindow : Window
{
    public MusicSourcesWindow(IMusicSourcesWindowVM viewModel)
    {
        InitializeComponent();

        DataContext = viewModel;
    }
}