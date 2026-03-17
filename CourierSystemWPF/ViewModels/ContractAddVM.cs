using CourierSystemWPF.Models;
using CourierSystemWPF.Utilities;
using CourierSystemWPF.Views;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CourierSystemWPF.ViewModels
{
    public class ContractAddVM : ViewModelBase
    {
        private DateTime _selectedStartDateTime;
        private DateTime _selectedEndDateTime;
        private Client? _selectedClient;
        private string? _notes;

        public ObservableCollection<Client> Clients { get; set; } = new ObservableCollection<Client>();

        public DateTime SelectedStartDateTime { get => _selectedStartDateTime; set { _selectedStartDateTime = value; OnPropertyChanged(); } }
        public DateTime SelectedEndDateTime { get => _selectedEndDateTime; set { _selectedEndDateTime = value; OnPropertyChanged(); } }
        public string? Notes { get => _notes; set { _notes = value; OnPropertyChanged(); } }
        public Client? SelectedClient { get => _selectedClient; set { _selectedClient = value; OnPropertyChanged(); } }

        // Commands
        public ICommand AddCommand { get; set; } = null!;
        public ICommand NewClientCommand { get; set; } = null!;

        private void UpdateClients()
        {
            ObservableCollection<Client> clientData = DBUtility.GetAllClients();
            Clients.Clear();
            foreach (Client client in clientData)
            {
                Clients.Add(client);
            }
        }

        private void CreateCommands()
        {
            AddCommand = new RelayCommand(obj =>
            {
                // Validate data
                // Presence
                if (
                    SelectedStartDateTime == null ||
                    SelectedEndDateTime == null ||
                    SelectedClient == null ||
                    Notes.IsNullOrEmpty()
                )
                {
                    MessageBox.Show("Missing/Empty fields");
                    return;
                }

                if (SelectedStartDateTime > SelectedEndDateTime)
                {
                    MessageBox.Show("End date must be after start date");
                    return;
                }

                DBUtility.AddNewContract(SelectedStartDateTime,SelectedEndDateTime,SelectedClient.id,Notes!);
                MessageBox.Show("Contract made!");
            });

            NewClientCommand = new RelayCommand(obj =>
            {
                var window = new NewClient();
                window.ShowDialog();
            });
        }

        public ContractAddVM() {
            CreateCommands();
            UpdateClients();

            SelectedStartDateTime = DateTime.Now;
            SelectedEndDateTime = DateTime.Now.AddHours(1);
        }
    }
}
