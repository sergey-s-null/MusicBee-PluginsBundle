using System.Windows;
using Module.MusicSourcesStorage.Gui.AbstractViewModels;

namespace Module.MusicSourcesStorage.Gui.Views;

public partial class Wizard : Window
{
    public Wizard(Func<Wizard, IWizardVM> viewModelFactory)
    {
        InitializeComponent();

        DataContext = viewModelFactory(this);
    }

    // todo поправить закрытие при нажатии на крестик в углу
    public void Close(bool askUser)
    {
        if (!askUser)
        {
            Dispatcher.Invoke(Close);
            return;
        }

        var result = MessageBox.Show(
            this,
            "Close wizard?",
            "(ಠ_ಠ)",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question
        );

        if (result == MessageBoxResult.Yes)
        {
            Close();
        }
    }
}