using CourierSystemWPF.Models;
using CourierSystemWPF.Utilities;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace CourierSystemWPF.ViewModels
{
    public class ReportMonthlyVM : ViewModelBase
    {
        private DateTime? _selectedDate;

        public ObservableCollection<Delivery> Deliveries { get; set; } = new ObservableCollection<Delivery>();
        public DateTime? SelectedDate { get { return _selectedDate; } set { _selectedDate = value; OnPropertyChanged(); } }

        public ICommand SearchCommand { get; set; } = null!;

        private void CreateCommands()
        {
            SearchCommand = new RelayCommand(obj =>
            {
                Deliveries.Clear();

                // Validation
                // Check if date selected
                if (SelectedDate == null)
                {
                    MessageBox.Show("Please select a date.");
                    return;
                }

                // Get deliveries from DB
                foreach (Delivery d in DBUtility.GetDeliveriesByMonth(SelectedDate.Value))
                {
                    Deliveries.Add(d);
                }
            });
        }

        public ReportMonthlyVM()
        {
            CreateCommands();
        }

    }
}