using CourierSystemWPF.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourierSystemWPF.ViewModels
{
    public class NavigationVM : ViewModelBase
    {
        private object _currentView = null!;

        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }


        public NavigationVM()
        {
            // Program essentially starts here
            // Button Commands

            // Default view
            Session.Navigation = this;
            CurrentView = new LoginVM();
        }
    }
}
