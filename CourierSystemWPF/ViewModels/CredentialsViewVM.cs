using CourierSystemWPF.Models;
using CourierSystemWPF.Utilities;
using CourierSystemWPF.Views;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CourierSystemWPF.ViewModels
{
    public class CredentialsViewVM : ViewModelBase
    {
        private string? _password;
        private string? _username;
        private string? _accessLevel;
        private string? _employeeID;
        private string? _credentialID;
        private Models.Login? _selectedItem;

        public ObservableCollection<Models.Login> Logins { get; set; } = new ObservableCollection<Models.Login>();

        // Text boxes
        public string? PasswordInput { get { return _password; } set { _password = value; OnPropertyChanged(); } }
        public string? UsernameInput { get { return _username; } set { _username = value; OnPropertyChanged(); } }
        public string? AccessLevelInput { get { return _accessLevel; } set { _accessLevel = value; OnPropertyChanged(); } }
        public string? EmployeeIDInput { get { return _employeeID; } set { _employeeID = value; OnPropertyChanged(); } }
        public string? CredentialIDInput { get { return _credentialID; } set { _credentialID = value; OnPropertyChanged(); } }

        // Commands
        public ICommand DeleteCommand { get; set; } = null!;
        public ICommand UpdateCommand { get; set; } = null!;

        public Models.Login? SelectedItem { get { return _selectedItem; } set {
                _selectedItem = value;
                if (_selectedItem != null) // Show data in editor
                {
                    PasswordInput = _selectedItem.password;
                    UsernameInput = _selectedItem.username;
                    CredentialIDInput = _selectedItem.id.ToString();
                    AccessLevelInput = _selectedItem.accessLevel.ToString();
                    EmployeeIDInput = _selectedItem.employeeId.ToString();
                }

                OnPropertyChanged(); 
            } }

        private void UpdateDataGrid()
        {
            // Populate collection
            Logins.Clear();
            foreach (Models.Login login in DBUtility.GetAllCredentials())
            {
                Logins.Add(login);
            }
        }

        private void CreateCommands()
        {
            DeleteCommand = new RelayCommand(obj =>
            {
                if (CredentialIDInput == null || CredentialIDInput.IsNullOrEmpty())
                {
                    return; 
                }

                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this item?","Confirmation",MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes) // Continue with deletion
                {
                    if (Session.Login.id == int.Parse(CredentialIDInput))
                    {
                        MessageBox.Show("Cannot delete logged in user.");
                        return;
                    }

                    DBUtility.DeleteCredentialWithId(int.Parse(CredentialIDInput));
                    UpdateDataGrid();
                }
            });

            UpdateCommand = new RelayCommand(obj =>
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
                try {
                    int.Parse(EmployeeIDInput);
                    int.Parse(AccessLevelInput);
                } catch
                {
                    isNumeric = false;
                }

                if (!isNumeric)
                {
                    MessageBox.Show("Access/Employee field not numeric");
                    return;
                }

                DBUtility.UpdateCredentialWithId(
                    int.Parse(CredentialIDInput),
                    UsernameInput,
                    PasswordInput,
                    int.Parse(AccessLevelInput),
                    int.Parse(EmployeeIDInput)
                );

                UpdateDataGrid();
                MessageBox.Show("Record updated.");
            });
        }

        public CredentialsViewVM() {
            UpdateDataGrid();
            CreateCommands();
        }
    }
}
