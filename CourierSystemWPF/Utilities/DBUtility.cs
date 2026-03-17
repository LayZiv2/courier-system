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
            conn.Close();
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
            conn.Close();
        }

        public static void AddCredentials(string username, string password, int accessLevel, int employeeId)
        {
            SqlConnection conn = CreateConnection();
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "INSERT INTO Credentials VALUES (@username, @password, @accessLevel, @employeeId);";
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.Parameters.AddWithValue("@accessLevel", accessLevel);
            cmd.Parameters.AddWithValue("@employeeId", employeeId);

            cmd.ExecuteNonQuery();
            conn.Close();
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

            conn.Close();
            return credentialData;
        }

        public static void AddNewClient(string firstName, string lastName, string email, string tel, string businessName)
        {
            SqlConnection conn = CreateConnection();
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "INSERT INTO Clients VALUES (@firstName, @lastName, @email, @tel, @businessName);";
            cmd.Parameters.AddWithValue("@firstName", firstName);
            cmd.Parameters.AddWithValue("@lastName", lastName);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@tel", tel);
            cmd.Parameters.AddWithValue("@businessName", businessName);

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static ObservableCollection<Client> GetAllClients()
        {
            ObservableCollection<Client> clientData = new ObservableCollection<Client>();

            SqlConnection conn = CreateConnection();
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM Clients";

            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Client newClient = new Client();
                newClient.id = (int)rdr[0];
                newClient.firstName = (string)rdr[1];
                newClient.lastName = (string)rdr[2];
                newClient.email = (string)rdr[3];
                newClient.phoneNumber = (string)rdr[4];
                newClient.businessName = (string)rdr[5];
                clientData.Add(newClient);
            }

            conn.Close();
            return clientData;
        }

        public static void AddNewContract(DateTime startDate, DateTime endDate, int clientId, string notes)
        {
            SqlConnection conn = CreateConnection();
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "INSERT INTO Contracts VALUES (@startDate, @endDate, @clientId, @notes);";
            cmd.Parameters.AddWithValue("@startDate", startDate);
            cmd.Parameters.AddWithValue("@endDate", endDate);
            cmd.Parameters.AddWithValue("@clientId", clientId);
            cmd.Parameters.AddWithValue("@notes", notes);

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static ObservableCollection<Contracts> GetAllContracts()
        {
            ObservableCollection<Contracts> contractData = new ObservableCollection<Contracts>();

            SqlConnection conn = CreateConnection();
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM Contracts";

            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Contracts newContract = new Contracts();
                newContract.id = (int)rdr[0];
                newContract.startDate = (DateTime)rdr[1];
                newContract.endDate = (DateTime)rdr[2];
                newContract.clientId = (int)rdr[3];
                newContract.notes = (string)rdr[4];
                contractData.Add(newContract);
            }

            conn.Close();
            return contractData;
        }

        public static void DeleteContractWithId(int id)
        {

            SqlConnection conn = CreateConnection();
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "DELETE FROM Contracts WHERE ContractID = @id;";
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static void UpdateContractWithId(int id, DateTime startDate, DateTime endDate, int clientId, string notes)
        {
            SqlConnection conn = CreateConnection();
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE Contracts SET ContractStartDate = @startDate, ContractEndDate = @endDate, ClientID = @clientId, Notes = @notes WHERE ContractID = @id;";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@startDate", startDate);
            cmd.Parameters.AddWithValue("@endDate", endDate);
            cmd.Parameters.AddWithValue("@clientId", clientId);
            cmd.Parameters.AddWithValue("@notes", notes);

            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
