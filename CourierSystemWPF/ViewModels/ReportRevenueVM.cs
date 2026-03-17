using CourierSystemWPF.Utilities;
using System;
using System.Windows;
using System.Windows.Input;

namespace CourierSystemWPF.ViewModels
{
    public class ReportRevenueVM : ViewModelBase
    {
        private DateTime? _selectedDate;
        private double _contractFee;
        private double _contractedDeliveries;
        private double _nonContractedDeliveries;
        private double _totalProfit;

        public DateTime? SelectedDate { get { return _selectedDate; } set { _selectedDate = value; OnPropertyChanged(); } }

        public double ContractFee { get { return _contractFee; } set { _contractFee = value; OnPropertyChanged(); } }
        public double ContractedDeliveries { get { return _contractedDeliveries; } set { _contractedDeliveries = value; OnPropertyChanged(); } }
        public double NonContractedDeliveries { get { return _nonContractedDeliveries; } set { _nonContractedDeliveries = value; OnPropertyChanged(); } }
        public double TotalProfit { get { return _totalProfit; } set { _totalProfit = value; OnPropertyChanged(); } }

        public ICommand CalculateCommand { get; set; }

        private void CreateCommands()
        {
            CalculateCommand = new RelayCommand(obj =>
            {
                if (SelectedDate == null)
                {
                    MessageBox.Show("Please select a date.");
                    return;
                }

                ContractFee = 0;
                ContractedDeliveries = 0;
                NonContractedDeliveries = 0;
                TotalProfit = 0;

                var data = DBUtility.GetMonthlyRevenueData((DateTime)SelectedDate);
                ContractFee = data.activeContracts * 50; // 50 per month
                ContractedDeliveries = data.contractedDeliveries * 2.5; // 2.5 per delivery
                NonContractedDeliveries = data.nonContractedDeliveries * 10; // 10 per delivery

                TotalProfit = Math.Round(ContractFee + ContractedDeliveries + NonContractedDeliveries,2);
            });
        }

        public ReportRevenueVM()
        {
            CreateCommands();
        }
    }
}