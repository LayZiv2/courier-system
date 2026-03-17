using CourierSystemWPF.Models;
using CourierSystemWPF.Utilities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace CourierSystemWPF.ViewModels
{
    public class ClientViewVM : ViewModelBase
    {
        private Client? _selectedItem;
        private string? _id;
        private string? _firstName;
        private string? _lastName;
        private string? _email;
        private string? _phone;
        private string? _business;

        public ObservableCollection<Client> Clients { get; set; } = new ObservableCollection<Client>();

        public string? IDInput { get { return _id; } set { _id = value; OnPropertyChanged(); } }
        public string? FirstName { get { return _firstName; } set { _firstName = value; OnPropertyChanged(); } }
        public string? LastName { get { return _lastName; } set { _lastName = value; OnPropertyChanged(); } }
        public string? Email { get { return _email; } set { _email = value; OnPropertyChanged(); } }
        public string? PhoneNumber { get { return _phone; } set { _phone = value; OnPropertyChanged(); } }
        public string? BusinessName { get { return _business; } set { _business = value; OnPropertyChanged(); } }

        // Commands
        public ICommand UpdateCommand { get; set; } = null!;
        public ICommand DeleteCommand { get; set; } = null!;

        public Client? SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;

                if (_selectedItem == null)
                {
                    IDInput = "";
                    FirstName = "";
                    LastName = "";
                    Email = "";
                    PhoneNumber = "";
                    BusinessName = "";
                    return;
                }

                IDInput = _selectedItem.id.ToString();
                FirstName = _selectedItem.firstName;
                LastName = _selectedItem.lastName;
                Email = _selectedItem.email;
                PhoneNumber = _selectedItem.phoneNumber;
                BusinessName = _selectedItem.businessName;

                OnPropertyChanged();
            }
        }

        private void UpdateDataGrid()
        {
            Clients.Clear();
            foreach (Client client in DBUtility.GetAllClients())
            {
                Clients.Add(client);
            }
        }

        private void CreateCommands()
        {
            UpdateCommand = new RelayCommand(obj =>
            {
                if (SelectedItem == null)
                {
                    MessageBox.Show("No item selected.");
                    return;
                }

                if (
                    FirstName.IsNullOrEmpty() ||
                    LastName.IsNullOrEmpty() ||
                    Email.IsNullOrEmpty() ||
                    PhoneNumber.IsNullOrEmpty() ||
                    BusinessName.IsNullOrEmpty()
                   )
                {
                    MessageBox.Show("Missing required fields.");
                    return;
                }

                DBUtility.UpdateClientWithId(
                    int.Parse(IDInput),
                    FirstName,
                    LastName,
                    Email,
                    PhoneNumber,
                    BusinessName
                );

                MessageBox.Show("Client Updated.");
                UpdateDataGrid();
            });

            DeleteCommand = new RelayCommand(obj =>
            {
                if (SelectedItem == null)
                    return;

                MessageBoxResult result = MessageBox.Show(
                    "Are you sure you want to delete this client?",
                    "Confirmation",
                    MessageBoxButton.YesNo
                );

                if (result == MessageBoxResult.Yes)
                {
                    DBUtility.DeleteClientWithId(int.Parse(IDInput));
                    UpdateDataGrid();
                }
            });
        }

        public ClientViewVM()
        {
            UpdateDataGrid();
            CreateCommands();
        }
    }
}