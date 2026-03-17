using CourierSystemWPF.Models;
using CourierSystemWPF.Utilities;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace CourierSystemWPF.ViewModels
{
    public class DeliveriesPersonalVM : ViewModelBase
    {
        private Delivery? _selectedAssignedItem;
        private Delivery? _selectedAcceptedItem;

        // Collections
        public ObservableCollection<Delivery> AssingedDeliveries { get; set; } = new ObservableCollection<Delivery>();
        public ObservableCollection<Delivery> AcceptedDeliveries { get; set; } = new ObservableCollection<Delivery>();

        // Selected Items
        public Delivery? SelectedAssignedItem
        {
            get => _selectedAssignedItem;
            set
            {
                _selectedAssignedItem = value;
                OnPropertyChanged();
            }
        }

        public Delivery? SelectedAcceptedItem
        {
            get => _selectedAcceptedItem;
            set
            {
                _selectedAcceptedItem = value;
                OnPropertyChanged();
            }
        }

        // Commands
        public ICommand AcceptCommand { get; set; } = null!;
        public ICommand DeclineCommand { get; set; } = null!;
        public ICommand CompleteCommand { get; set; } = null!;

        // Constructor
        public DeliveriesPersonalVM()
        {
            LoadDeliveries();
            CreateCommands();
        }

        private void LoadDeliveries()
        {
            AssingedDeliveries.Clear();
            AcceptedDeliveries.Clear();

            foreach (var delivery in DBUtility.GetAssignedDeliveries(Session.Login.employeeId))
            {
                if (delivery.accepted == 1) { 
                    if (delivery.delivered != 1)
                    {
                        AcceptedDeliveries.Add(delivery);
                    }
                }
                else { 
                    AssingedDeliveries.Add(delivery);
                }
            }
        }

        private void CreateCommands()
        {
            AcceptCommand = new RelayCommand(obj =>
            {
                if (SelectedAssignedItem == null)
                {
                    MessageBox.Show("No assigned delivery selected.");
                    return;
                }

                DBUtility.AcceptDelivery(SelectedAssignedItem.id);
                MessageBox.Show("Delivery accepted.");
                LoadDeliveries();
            });

            DeclineCommand = new RelayCommand(obj =>
            {
                if (SelectedAssignedItem == null)
                {
                    MessageBox.Show("No assigned delivery selected.");
                    return;
                }

                MessageBoxResult result = MessageBox.Show(
                    "Are you sure you want to decline this delivery?",
                    "Confirmation",
                    MessageBoxButton.YesNo
                );

                if (result == MessageBoxResult.Yes)
                {
                    DBUtility.DeclineDelivery(SelectedAssignedItem.id);
                    LoadDeliveries();
                }
            });

            CompleteCommand = new RelayCommand(obj =>
            {
                if (SelectedAcceptedItem == null)
                {
                    MessageBox.Show("No accepted delivery selected.");
                    return;
                }

                DBUtility.MarkDeliveryComplete(SelectedAcceptedItem.id); 
                MessageBox.Show("Delivery marked as complete.");
                LoadDeliveries();
            });
        }
    }
}