using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CourierSystemWPF.Models
{
    public class Login
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int accessLevel { get; set; }
        public int? employeeId { get; set; }
    }
}
