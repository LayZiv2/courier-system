using CourierSystemWPF.Models;
using CourierSystemWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierSystemWPF.Utilities
{
    public class Session
    {
        private static Login? login;
        private static NavigationVM? navigation;

        public static Login? Login {
            get { return login; }
            set { login = value; }
        }
        public static NavigationVM? Navigation
        {
            get { return navigation; }
            set { navigation = value; }
        }
    }
}
