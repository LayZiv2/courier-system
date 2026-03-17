using CourierSystemWPF.Utilities;
using CourierSystemWPF.ViewModels;
using CourierSystemWPF.Views;
using System.Windows.Input;

namespace CourierSystemWPF.ViewModels
{
    public class ReportsVM : ViewModelBase
    {
        private object _currentSubView = null!;

        // Commands
        public ICommand HomeCommand { get; set; } = null!;
        public ICommand DailyCommand { get; set; } = null!;
        public ICommand MonthlyCommand { get; set; } = null!;
        public ICommand RevenueCommand { get; set; } = null!;

        public object CurrentSubView
        {
            get { return _currentSubView; }
            set { _currentSubView = value; OnPropertyChanged(); }
        }

        private void CreateCommands()
        {
            HomeCommand = new RelayCommand(obj =>
            {
                Session.Navigation.CurrentView = new HomeVM();
            });

            DailyCommand = new RelayCommand(obj =>
            {
                CurrentSubView = new ReportDailyVM();
            });

            MonthlyCommand = new RelayCommand(obj => 
            {
                CurrentSubView = new ReportMonthlyVM();
            });

            RevenueCommand = new RelayCommand(obj => 
            {
                CurrentSubView = new ReportRevenueVM();
            });

        }

        public ReportsVM()
        {
            CreateCommands();
        }
    }
}
