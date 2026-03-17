using CourierSystemWPF.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourierSystemWPF.ViewModels
{
    public class DeliveriesVM : ViewModelBase
    {
        private object _currentSubView = null!;

        // Commands
        public ICommand ViewPersonalCommand { get; set; } = null!;
        public ICommand ViewAllCommand { get; set; } = null!;
        public ICommand AssignCommand { get; set; } = null!;
        public ICommand AddCommand { get; set; } = null!;
        public ICommand HomeCommand { get; set; } = null!;

        public object CurrentSubView
        {
            get { return _currentSubView; }
            set { _currentSubView = value; OnPropertyChanged(); }
        }

        private void CreateCommands()
        {

            AddCommand = new RelayCommand(obj =>
            {
                if (Session.Login.accessLevel >= 1)
                {
                    CurrentSubView = new DeliveriesAddVM();
                }
            });

            ViewPersonalCommand = new RelayCommand(obj =>
            {
                CurrentSubView = new DeliveriesPersonalVM();
            });

            HomeCommand = new RelayCommand(obj =>
            {
                Session.Navigation.CurrentView = new HomeVM();
            });

            ViewAllCommand = new RelayCommand(obj =>
            {
                if (Session.Login.accessLevel >= 1)
                {
                    CurrentSubView = new DeliveriesViewVM();
                }
            });
        }

        public DeliveriesVM()
        {
            CreateCommands();
        }
    }
}
