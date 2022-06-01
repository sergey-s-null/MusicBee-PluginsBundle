using System.Windows;
using Module.AudioSourcesComparer.GUI.AbstractViewModels;

namespace Module.AudioSourcesComparer.GUI.Views
{
    public partial class VkToLocalComparerWindow : Window
    {
        public VkToLocalComparerWindow(IVkToLocalComparerWindowVM viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}