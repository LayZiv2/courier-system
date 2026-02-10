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
    internal class Login
    {
        public string username { get; set; }
        public string password { get; set; }
        public int accessLevel { get; set; }
        public int? employeeId { get; set; }

        public bool ValidateCredentials()
        {
            string connString = ConfigurationManager.ConnectionStrings["CourierDB"].ConnectionString;
            SqlConnection conn = new SqlConnection(connString);

            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM Credentials WHERE Username = @username AND Password = @password";
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);

            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read()) // If found entry
            {
                conn.Close();
                return true;
            }
            else
            {
                conn.Close();
                return false;
            }
        }
    }
}
