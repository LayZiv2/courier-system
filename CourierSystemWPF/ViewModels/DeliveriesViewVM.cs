using CourierSystemWPF.Models;
using CourierSystemWPF.Utilities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace CourierSystemWPF.ViewModels
{
    public class DeliveriesViewVM : ViewModelBase
    {
        private Delivery? _selectedDelivery;
        private string? _deliveryID;
        private string? _destinationAddress;
        private DateTime? _deliveryDateTime;
        private int? _courierId;
        private string? _contractId;

        public ObservableCollection<Delivery> Deliveries { get; set; } = new ObservableCollection<Delivery>();
        public ObservableCollection<Courier> Couriers { get; set; } = new ObservableCollection<Courier>();

        // Sidebar properties
        public string? DeliveryIDInput { get { return _deliveryID; } set { _deliveryID = value; OnPropertyChanged(); } }
        public string? DestinationAddress { get { return _destinationAddress; } set { _destinationAddress = value; OnPropertyChanged(); } }
        public DateTime? DeliveryDateTime { get { return _deliveryDateTime; } set { _deliveryDateTime = value; OnPropertyChanged(); } }
        public int? CourierId { get { return _courierId; } set { _courierId = value; OnPropertyChanged(); } }
        public string? ContractIdInput { get { return _contractId; } set { _contractId = value; OnPropertyChanged(); } }

        public Delivery? SelectedDelivery
        {
            get { return _selectedDelivery; }
            set
            {
                _selectedDelivery = value;

                if (_selectedDelivery == null)
                {
                    DeliveryIDInput = "";
                    DestinationAddress = "";
                    DeliveryDateTime = null;
                    CourierId = null;
                    ContractIdInput = "";
                    return;
                }

                DeliveryIDInput = _selectedDelivery.id.ToString();
                DestinationAddress = _selectedDelivery.destinationAddress;
                DeliveryDateTime = _selectedDelivery.deliveryDateTime;
                CourierId = _selectedDelivery.courierId;
                ContractIdInput = _selectedDelivery.contractId.ToString();

                OnPropertyChanged();
            }
        }

        // Commands
        public ICommand UpdateCommand { get; set; } = null!;
        public ICommand DeleteCommand { get; set; } = null!;

        public DeliveriesViewVM()
        {
            UpdateDataGrid();
            LoadCouriers();
            CreateCommands();
        }

        private void UpdateDataGrid()
        {
            Deliveries.Clear();
            foreach (Delivery d in DBUtility.GetAllDeliveries())
            {
                Deliveries.Add(d);
            }
        }

        private void LoadCouriers()
        {
            Couriers.Clear();
            foreach (Courier c in DBUtility.GetAllCouriers())
            {
                Couriers.Add(c);
            }
        }

        private void CreateCommands()
        {
            UpdateCommand = new RelayCommand(obj =>
            {
                if (SelectedDelivery == null)
                {
                    MessageBox.Show("No delivery selected.");
                    return;
                }

                if (DestinationAddress.IsNullOrEmpty() || CourierId == null || DeliveryDateTime == null || ContractIdInput.IsNullOrEmpty())
                {
                    MessageBox.Show("Please fill all fields.");
                    return;
                }

                DBUtility.UpdateDeliveryWithId(
                    SelectedDelivery.id,
                    DestinationAddress,
                    DeliveryDateTime.Value,
                    CourierId.Value,
                    int.Parse(ContractIdInput)
                );

                MessageBox.Show("Delivery updated.");
                UpdateDataGrid();
            });

            DeleteCommand = new RelayCommand(obj =>
            {
                if (SelectedDelivery == null)
                    return;

                MessageBoxResult result = MessageBox.Show(
                    "Are you sure you want to delete this delivery?",
                    "Confirmation",
                    MessageBoxButton.YesNo
                );

                if (result == MessageBoxResult.Yes)
                {
                    DBUtility.DeleteDeliveryWithId(SelectedDelivery.id);
                    UpdateDataGrid();
                }
            });
        }
    }
}