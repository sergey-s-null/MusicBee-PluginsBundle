using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VkMusicDownloader.GUI
{
    public class AuthDialogVM : BaseViewModel
    {
        private string _login = "";
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                NotifyPropChanged(nameof(Login));
            }
        }

        private string _password = "";
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                NotifyPropChanged(nameof(Password));
            }
        }
    }
}
