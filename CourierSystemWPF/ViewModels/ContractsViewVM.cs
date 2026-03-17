using CourierSystemWPF.Models;
using CourierSystemWPF.Utilities;
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
    public class ContractsViewVM : ViewModelBase
    {
        private DateTime _endDate;
        private DateTime _startDate;
        private Client? _clientObj;
        private string? _notes;
        private string? _contractId;
        private Models.Contracts? _selectedItem;

        public ObservableCollection<Models.Contracts> Contracts { get; set; } = new ObservableCollection<Models.Contracts>();
        public ObservableCollection<Models.Client> Clients { get; set; } = new ObservableCollection<Models.Client>();

        // Text boxes
        public DateTime EndDate { get { return _endDate; } set { _endDate = value; OnPropertyChanged(); } }
        public DateTime StartDate { get { return _startDate; } set { _startDate = value; OnPropertyChanged(); } }
        public string? Notes { get { return _notes; } set { _notes = value; OnPropertyChanged(); } }
        public Client? ClientID { get { return _clientObj; } set { _clientObj = value; OnPropertyChanged(); } }
        public string? ContractIDInput { get { return _contractId; } set { _contractId = value; OnPropertyChanged(); } }

        public Models.Contracts? SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;

                if (_selectedItem == null) // Clear the values
                {
                    EndDate = DateTime.Now;
                    StartDate = DateTime.Now;
                    Notes = "";
                    ClientID = null;
                    ContractIDInput = "";
                    return;
                }

                Clients.Clear();
                ObservableCollection<Models.Client> ClientData = DBUtility.GetAllClients();
                foreach (var client in ClientData)
                {
                    Clients.Add(client);
                }

                EndDate = _selectedItem!.endDate;
                StartDate = _selectedItem.startDate;
                Notes = _selectedItem.notes;
                ClientID = Clients.FirstOrDefault(c => c.id == _selectedItem.clientId);
                ContractIDInput = _selectedItem.id.ToString();

                OnPropertyChanged();
            }
        }

        // Commands
        public ICommand DeleteCommand { get; set; } = null!;
        public ICommand UpdateCommand { get; set; } = null!;

        private void UpdateDataGrid()
        {
            // Populate collection
            Contracts.Clear();
            foreach (Models.Contracts contract in DBUtility.GetAllContracts())
            {
                Contracts.Add(contract);
            }
        }

        private void CreateCommands()
        {
            UpdateCommand = new RelayCommand(obj =>
            {
                // Validate
                if (SelectedItem == null) // Check if something is selected
                {
                    MessageBox.Show("No item selected.");
                    return;
                }

                if ( // Presence check
                        ContractIDInput.IsNullOrEmpty() ||
                        Notes.IsNullOrEmpty()
                    )
                {
                    MessageBox.Show("Missing Fields");
                    return;
                }

                // Date check
                if (StartDate > EndDate)
                {
                    MessageBox.Show("Start date cannot be further than end date.");
                    return;
                }

                DBUtility.UpdateContractWithId(int.Parse(ContractIDInput), StartDate, EndDate, ClientID.id, Notes);
                MessageBox.Show("Contract Updated.");

                UpdateDataGrid();
            });

            DeleteCommand = new RelayCommand(obj =>
            {
                if (SelectedItem == null) // Check if something selected
                {
                    return;
                }

                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this item?", "Confirmation", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes) // Continue with deletion
                {
                    DBUtility.DeleteContractWithId(int.Parse(ContractIDInput));
                    UpdateDataGrid();
                }
            });
        }

        public ContractsViewVM()
        {
            UpdateDataGrid();
            CreateCommands();
        }
    }
}
