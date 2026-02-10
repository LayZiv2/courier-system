using CourierSystemWPF.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierSystemWPF.ViewModels
{
    public class HomeVM : ViewModelBase
    {
        private object _currentHomeView = null!;

        public object CurrentHomeView
        {
            get { return _currentHomeView; }
            set { _currentHomeView = value; OnPropertyChanged(); }
        }

        public HomeVM() {
            CurrentHomeView = new ViewDeliveriesVM();
        }
    }
}
