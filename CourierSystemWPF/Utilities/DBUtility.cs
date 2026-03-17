using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourierSystemWPF.Models;
using System.Collections.ObjectModel;

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
                login.id = (int) rdr[0]; // 0st index meaning id
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

        public static void DeleteCredentialWithId(int id)
        {

            SqlConnection conn = CreateConnection();
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "DELETE FROM Credentials WHERE CredentialID = @id;";
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
        }

        public static void UpdateCredentialWithId(int id, string username, string password, int accessLevel, int employeeId)
        {
            SqlConnection conn = CreateConnection();
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE Credentials SET Username = @username, Password = @password, AccessLevel = @accessLevel, EmployeeID = @employeeId WHERE CredentialID = @id;";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.Parameters.AddWithValue("@accessLevel", accessLevel);
            cmd.Parameters.AddWithValue("@employeeId", employeeId);

            cmd.ExecuteNonQuery();
        }

        public static ObservableCollection<Login> GetAllCredentials()
        {
            ObservableCollection<Login>  credentialData = new ObservableCollection<Login>();

            SqlConnection conn = CreateConnection();
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM Credentials";

            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Login newLogin = new Login();
                newLogin.id = (int) rdr[0];
                newLogin.username = (string) rdr[1];
                newLogin.password = (string) rdr[2];
                newLogin.accessLevel = (int) rdr[3];
                newLogin.employeeId = (int) rdr[4];
                credentialData.Add(newLogin);
            }
            return credentialData;
        }
    }
}
