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
    public class NewClientVM : ViewModelBase
    {
        private string? _firstName;
        private string? _lastName;
        private string? _email;
        private string? _tel;
        private string? _business;

        // Text boxes
        public string? FirstNameInput { get { return _firstName; } set { _firstName = value; OnPropertyChanged(); } }
        public string? LastNameInput { get { return _lastName; } set { _lastName = value; OnPropertyChanged(); } }
        public string? EmailInput { get { return _email; } set { _email = value; OnPropertyChanged(); } }
        public string? TelInput { get { return _tel; } set { _tel = value; OnPropertyChanged(); } }
        public string? BusinessInput { get { return _business; } set { _business = value; OnPropertyChanged(); } }

        public ICommand AddCommand { get; set; } = null!;
        public ICommand CancelCommand { get; set; } = null!;

        private void CreateCommands()
        {
            AddCommand = new RelayCommand(obj =>
            {
                // Validate data
                // Presence
                if (
                FirstNameInput.IsNullOrEmpty() ||
                LastNameInput.IsNullOrEmpty() ||
                EmailInput.IsNullOrEmpty() ||
                TelInput.IsNullOrEmpty() ||
                BusinessInput.IsNullOrEmpty()
                )
                {
                    MessageBox.Show("Missing/Empty fields");
                    return;
                }

                // TODO Email validation
                DBUtility.AddNewClient(FirstNameInput, LastNameInput, EmailInput, TelInput, BusinessInput);
                MessageBox.Show("New Client Added.");

                if (obj is Window window)
                {
                    window.Close();
                }
            });

            CancelCommand = new RelayCommand(obj => {
                if (obj is Window window)
                {
                    window.Close();
                }
            });
        }

        public NewClientVM()
        {
            CreateCommands();
        }
    }
}
