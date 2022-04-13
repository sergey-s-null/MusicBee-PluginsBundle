using System.Windows;
using Module.VkAudioDownloader.GUI.ViewModels;

namespace Module.VkAudioDownloader.GUI.Views
{
    /// <summary>
    /// Логика взаимодействия для AuthDialog.xaml
    /// </summary>
    public partial class AuthDialog : Window
    {
        public string Login
        {
            get => _viewModel.Login;
            set => _viewModel.Login = value;
        }

        public string Password
        {
            get => _viewModel.Password;
            set => _viewModel.Password = value;
        }

        private AuthDialogVM _viewModel = new AuthDialogVM();

        public AuthDialog()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }

        public bool ShowDialog(out string? login, out string? password)
        {
            Login = "";
            Password = "";
            var res = ShowDialog();
            if (res == true)
            {
                login = Login;
                password = Password;
                return true;
            }
            
            login = null;
            password = null;
            return false;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
