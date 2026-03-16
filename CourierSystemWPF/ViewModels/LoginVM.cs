using CourierSystemWPF.Models;
using CourierSystemWPF.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourierSystemWPF.ViewModels
{
    public class LoginVM : ViewModelBase
    {
        private Login _login;
        private string _statusText = "...";

        private string? _username;
        private string? _password;

        public ICommand LoginCommand { get; set; } = null!;
        public string StatusText { get { return _statusText; } set { _statusText = value; OnPropertyChanged(); } }

        // Binded to textboxes
        public string? PasswordInput { get { return _password; } set { _password = value; OnPropertyChanged(); } }
        public string? UsernameInput { get { return _username; } set { _username = value; OnPropertyChanged(); } }

        public LoginVM()    
        {
            _login = new Login();

            // Command Functions //
            LoginCommand = new RelayCommand(obj =>
            {
                if (_username == null || _password == null)
                {
                    StatusText = "Invalid username/password";
                    return; // Finish method
                }

                _login.username = _username;
                _login.password = _password;

                bool result = DBUtility.ValidateCredentials(_username, _password);
                if (result)
                {
                    StatusText = "Logged in";
                    Session.Login = _login;
                    if (Session.Navigation != null)
                    {
                        Session.Navigation.CurrentView = new HomeVM();
                    }
                } else
                {
                    StatusText = "Failed!";
                }
            });
        }
    }
}
