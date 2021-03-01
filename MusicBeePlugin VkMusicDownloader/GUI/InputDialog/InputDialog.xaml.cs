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

namespace VkMusicDownloader.GUI
{
    /// <summary>
    /// Логика взаимодействия для InputDialog.xaml
    /// </summary>
    public partial class InputDialog : Window
    {
        public string TitleText
        {
            get => _viewModel.TitleText;
            set => _viewModel.TitleText = value;
        }

        public string InputText
        {
            get => _viewModel.InputText;
            set => _viewModel.InputText = value;
        }

        private InputDialogVM _viewModel = new InputDialogVM();

        public InputDialog()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }

        public bool ShowDialog(string titleText, out string inputText)
        {
            TitleText = titleText;
            return ShowDialog(out inputText);
        }

        public bool ShowDialog(out string inputText)
        {
            bool? res = ShowDialog();
            if (res == true)
            {
                inputText = InputText;
                return true;
            }
            else
            {
                inputText = null;
                return false;
            }
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
