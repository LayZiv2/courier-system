using CourierSystemWPF.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CourierSystemWPF.ViewModels
{
    public class HomeVM : ViewModelBase
    {
        private string _displayUsername = "No User";
        public string DisplayUsername { get { return _displayUsername; } set { _displayUsername = value; OnPropertyChanged(); } }

        // Button commands
        public ICommand LogoutCommand { get; set; } = null!;
        public ICommand CredentialsCommand { get; set; } = null!;

        // Button visibility
        private Visibility _showDeliveries = Visibility.Collapsed;
        private Visibility _showContracts = Visibility.Collapsed;
        private Visibility _showEmployees = Visibility.Collapsed;
        private Visibility _showCredentials = Visibility.Collapsed;

        public Visibility ShowDeliveries { get { return _showDeliveries; } set { _showDeliveries = value; OnPropertyChanged(); } }
        public Visibility ShowContracts { get { return _showContracts; } set { _showContracts = value; OnPropertyChanged(); } }
        public Visibility ShowEmployees { get { return _showEmployees; } set { _showEmployees = value; OnPropertyChanged(); } }
        public Visibility ShowCredentials { get { return _showCredentials; } set { _showCredentials = value; OnPropertyChanged(); } }

        private void CreateCommands()
        {
            LogoutCommand = new RelayCommand(obj =>
            {
                Session.Login = null;
                Session.Navigation.CurrentView = new LoginVM();
            });

            CredentialsCommand = new RelayCommand(obj => {
                Session.Navigation.CurrentView = new CredentialsVM();
            });
        }

        private void ConfigureButtonVisibility()
        {
            int alevel = Session.Login.accessLevel;
            switch (alevel)
            {
                case 0: // Courier
                    ShowDeliveries = Visibility.Visible;
                    ShowContracts = Visibility.Collapsed;
                    ShowEmployees = Visibility.Collapsed;
                    ShowCredentials = Visibility.Collapsed;
                    break;
                case 1: // Logistics Coordinator
                    ShowDeliveries = Visibility.Visible;
                    ShowContracts = Visibility.Visible;
                    ShowEmployees = Visibility.Collapsed;
                    ShowCredentials = Visibility.Collapsed;
                    break;
                case 2: // Admin
                    ShowDeliveries = Visibility.Visible;
                    ShowContracts = Visibility.Visible;
                    ShowEmployees = Visibility.Visible;
                    ShowCredentials = Visibility.Visible;
                    break;
                case 10: // Owner
                    ShowDeliveries = Visibility.Visible;
                    ShowContracts = Visibility.Visible;
                    ShowEmployees = Visibility.Visible;
                    ShowCredentials = Visibility.Visible;
                    break;
            }

        }
        public HomeVM() {


            if (Session.Login == null)
            {
                MessageBox.Show("No user. Returning to login.");
                Session.Navigation.CurrentView = new LoginVM();
                return;
            }

            DisplayUsername = Session.Login.username;

            CreateCommands();
            ConfigureButtonVisibility();
        }
    }
}
