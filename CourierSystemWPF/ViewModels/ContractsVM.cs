using CourierSystemWPF.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourierSystemWPF.ViewModels
{
    class ContractsVM : ViewModelBase
    {
        private object _currentSubView = null!;

        public object CurrentSubView
        {
            get { return _currentSubView; }
            set { _currentSubView = value; OnPropertyChanged(); }
        }

        public ICommand HomeCommand { get; set; } = null!;
        public ICommand AddContractCommand { get; set; } = null!;
        public ICommand ViewContractCommand { get; set; } = null!;

        private void CreateCommands()
        {
            HomeCommand = new RelayCommand(obj =>
            {
                Session.Navigation!.CurrentView = new HomeVM();
            });


            AddContractCommand = new RelayCommand(obj =>
            {
                CurrentSubView = new ContractAddVM();
            });
        }

        public ContractsVM() { 
            CreateCommands();
        }
    }
}
