using CourierSystemWPF.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierSystemWPF.ViewModels
{
    public class CredentialsVM : ViewModelBase
    {
        private object _currentSubView = null!;

        public object CurrentSubView
        {
            get { return _currentSubView; }
            set { _currentSubView = value; OnPropertyChanged(); }
        }

        public CredentialsVM() {
            CurrentSubView = new CredentialsViewVM()!;
        }
    }
}
