using CourierSystemWPF.Models;
using CourierSystemWPF.Utilities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace CourierSystemWPF.ViewModels
{
    public class DeliveriesAddVM : ViewModelBase
    {
        private DateTime _deliveryDateTime;
        private Courier? _selectedCourier;
        private Contracts? _selectedContract;
        private string? _destinationAddress;
        private bool _delivered;

        public ObservableCollection<Courier> Couriers { get; set; } = new ObservableCollection<Courier>();
        public ObservableCollection<Contracts> Contracts { get; set; } = new ObservableCollection<Contracts>();

        public DateTime DeliveryDateTime
        {
            get => _deliveryDateTime;
            set { _deliveryDateTime = value; OnPropertyChanged(); }
        }

        public Courier? SelectedCourier
        {
            get => _selectedCourier;
            set { _selectedCourier = value; OnPropertyChanged(); }
        }

        public Contracts? SelectedContract
        {
            get => _selectedContract;
            set { _selectedContract = value; OnPropertyChanged(); }
        }

        public string? DestinationAddress
        {
            get => _destinationAddress;
            set { _destinationAddress = value; OnPropertyChanged(); }
        }

        public bool Delivered
        {
            get => _delivered;
            set { _delivered = value; OnPropertyChanged(); }
        }

        // Commands
        public ICommand AddCommand { get; set; } = null!;

        private void UpdateData()
        {
            // Couriers
            Couriers.Clear();
            foreach (Courier courier in DBUtility.GetAllCouriers())
            {
                Couriers.Add(courier);
            }

            // Contracts
            Contracts.Clear();
            foreach (Contracts contract in DBUtility.GetAllContracts())
            {
                Contracts.Add(contract);
            }
        }

        private void CreateCommands()
        {
            AddCommand = new RelayCommand(obj =>
            {
                // Validation
                if (
                    SelectedCourier == null ||
                    DestinationAddress.IsNullOrEmpty()
                )
                {
                    MessageBox.Show("Missing/Empty fields");
                    return;
                }

                // Contract is optional
                int contractId = SelectedContract != null ? SelectedContract.id : 0;

                DBUtility.AddNewDelivery(
                    DeliveryDateTime,
                    contractId,
                    Delivered ? 1 : 0,
                    SelectedCourier.id,
                    DestinationAddress!
                );

                MessageBox.Show("Delivery created!");
            });
        }

        public DeliveriesAddVM()
        {
            CreateCommands();
            UpdateData();

            DeliveryDateTime = DateTime.Now;
            Delivered = false;
        }
    }
}