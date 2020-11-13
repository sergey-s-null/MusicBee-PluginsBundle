using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MusicBeePlugin_VkMusicDownloader
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

        private AuthDialogViewModel _viewModel = new AuthDialogViewModel();

        public AuthDialog()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }

        public bool ShowDialog(out string login, out string password)
        {
            Login = "";
            Password = "";
            bool? res = ShowDialog();
            if (res == true)
            {
                login = Login;
                password = Password;
                return true;
            }
            else
            {
                login = null;
                password = null;
                return false;
            }
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
