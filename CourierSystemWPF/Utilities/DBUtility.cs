using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourierSystemWPF.Models;

namespace CourierSystemWPF.Utilities
{
    public class DBUtility
    {
        private static SqlConnection CreateConnection()
        {
            string connString = ConfigurationManager.ConnectionStrings["CourierDB"].ConnectionString;
            SqlConnection conn = new SqlConnection(connString);
            return conn;
        }

        public static bool ValidateCredentials(string username, string password, Login login)
        {
            SqlConnection conn = CreateConnection();
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM Credentials WHERE Username = @username AND Password = @password";
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);

            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read()) // If found entry
            {
                login.accessLevel = (int) rdr[3]; // 3rd index meaning access level
                login.employeeId = (int) rdr[4];  // 4th index meaning employee id
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
