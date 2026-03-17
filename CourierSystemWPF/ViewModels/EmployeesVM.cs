using CourierSystemWPF.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourierSystemWPF.ViewModels
{
    public class EmployeesVM : ViewModelBase
    {
        private object _currentSubView = null!;

        // Commands
        public ICommand ViewBreaksCommand { get; set; } = null!;
        public ICommand HomeCommand { get; set; } = null!;

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
        }

        public EmployeesVM()
        {
            CreateCommands();
        }
    }
}
