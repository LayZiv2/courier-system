using CourierSystemWPF.Utilities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CourierSystemWPF.ViewModels
{
    public class CredentialsAddVM : ViewModelBase
    {
        private string? _password;
        private string? _username;
        private string? _accessLevel;
        private string? _employeeID;

        // Text boxes
        public string? PasswordInput { get { return _password; } set { _password = value; OnPropertyChanged(); } }
        public string? UsernameInput { get { return _username; } set { _username = value; OnPropertyChanged(); } }
        public string? AccessLevelInput { get { return _accessLevel; } set { _accessLevel = value; OnPropertyChanged(); } }
        public string? EmployeeIDInput { get { return _employeeID; } set { _employeeID = value; OnPropertyChanged(); } }

        // Commands
        public ICommand AddCommand { get; set; } = null!;

        private void CreateCommands()
        {
            AddCommand = new RelayCommand(obj =>
            {
                // Validate data
                // Presence
                if (
                UsernameInput.IsNullOrEmpty() ||
                PasswordInput.IsNullOrEmpty() ||
                AccessLevelInput.IsNullOrEmpty() ||
                EmployeeIDInput.IsNullOrEmpty()
                )
                {
                    MessageBox.Show("Missing/Empty fields");
                    return;
                }

                // Numeric
                bool isNumeric = true;
                try
                {
                    int.Parse(EmployeeIDInput);
                    int.Parse(AccessLevelInput);
                }
                catch
                {
                    isNumeric = false;
                }

                if (!isNumeric)
                {
                    MessageBox.Show("Access/Employee field not numeric");
                    return;
                }

                DBUtility.AddCredentials(UsernameInput, PasswordInput, int.Parse(AccessLevelInput), int.Parse(EmployeeIDInput));
                MessageBox.Show("Added new credential.");
            });
        }

        public CredentialsAddVM() {
            CreateCommands();
        }
    }
}
