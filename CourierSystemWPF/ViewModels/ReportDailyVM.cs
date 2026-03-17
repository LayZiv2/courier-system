using CourierSystemWPF.Models;
using CourierSystemWPF.Utilities;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace CourierSystemWPF.ViewModels
{
    public class ReportDailyVM : ViewModelBase
    {
        private Courier? _courierID;
        private DateTime? _selectedDate;

        public ObservableCollection<Delivery> Deliveries { get; set; } = new ObservableCollection<Delivery>();
        public ObservableCollection<Courier> Couriers { get; set; } = new ObservableCollection<Courier>();

        public Courier? CourierID
        {
            get { return _courierID; }
            set { _courierID = value; OnPropertyChanged(); }
        }

        public DateTime? SelectedDate
        {
            get { return _selectedDate; }
            set { _selectedDate = value; OnPropertyChanged(); }
        }

        public ICommand SearchCommand { get; set; } = null!;

        public ReportDailyVM()
        {
            LoadCouriers();
            CreateCommands();
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
            SearchCommand = new RelayCommand(obj =>
            {
                Deliveries.Clear();


                // Validation
                // Check if courier selected
                if (CourierID == null)
                {
                    MessageBox.Show("Please select a courier.");
                    return;
                }

                // Check if date selected
                if (SelectedDate == null)
                {
                    MessageBox.Show("Please select a date.");
                    return;
                }

                // Get deliveries from DB
                foreach (Delivery d in DBUtility.GetCourierDeliveriesByDay(CourierID.id, SelectedDate.Value))
                {
                    Deliveries.Add(d);
                }
            });
        }
    }
}