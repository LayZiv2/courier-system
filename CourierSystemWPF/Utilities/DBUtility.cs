using CourierSystemWPF.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
                login.id = (int)rdr[0]; // 0st index meaning id
                login.accessLevel = (int)rdr[3]; // 3rd index meaning access level
                login.employeeId = (int)rdr[4];  // 4th index meaning employee id
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
            ObservableCollection<Login> credentialData = new ObservableCollection<Login>();

            SqlConnection conn = CreateConnection();
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM Credentials";

            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Login newLogin = new Login();
                newLogin.id = (int)rdr[0];
                newLogin.username = (string)rdr[1];
                newLogin.password = (string)rdr[2];
                newLogin.accessLevel = (int)rdr[3];
                newLogin.employeeId = (int)rdr[4];
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

        public static void UpdateClientWithId(int id, string firstName, string lastName, string email, string phone, string business)
        {
            SqlConnection conn = CreateConnection();
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"UPDATE Clients SET FirstName = @firstName, LastName = @lastName, Email = @email, PhoneNumber = @phone, BusinessName = @business WHERE ClientID = @id";

            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@firstName", firstName);
            cmd.Parameters.AddWithValue("@lastName", lastName);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@phone", phone ?? "");
            cmd.Parameters.AddWithValue("@business", business ?? "");

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static void DeleteClientWithId(int id)
        {
            SqlConnection conn = CreateConnection();
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM Clients WHERE ClientID = @id";
            cmd.Parameters.AddWithValue("@id", id);

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

        public static ObservableCollection<Courier> GetAllCouriers()
        {
            ObservableCollection<Courier> courierData = new ObservableCollection<Courier>();

            SqlConnection conn = CreateConnection();
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM Couriers";

            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Courier newCourier = new Courier();
                newCourier.id = (int)rdr[0];
                newCourier.completedOrders = (int)rdr[1];
                newCourier.cancelledOrders = (int)rdr[2];
                newCourier.employeeId = (int)rdr[3];
                courierData.Add(newCourier);
            }

            conn.Close();
            return courierData;
        }

        public static void AddNewDelivery(DateTime dateTime, int contractId, int delivered, int courierId, string address)
        {
            SqlConnection conn = CreateConnection();
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = @"INSERT INTO Deliveries 
        (DestinationAddress, DeliveryDateTime, ContractID, Delivered, CourierID, Accepted)
        VALUES (@address, @dateTime, @contractId, @delivered, @courierId, @accepted)";

            cmd.Parameters.AddWithValue("@address", address);
            cmd.Parameters.AddWithValue("@dateTime", dateTime);
            cmd.Parameters.AddWithValue("@contractId", contractId);
            cmd.Parameters.AddWithValue("@delivered", delivered);
            cmd.Parameters.AddWithValue("@courierId", courierId);
            cmd.Parameters.AddWithValue("@accepted", 0);

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static ObservableCollection<Delivery> GetAssignedDeliveries(int employeeId)
        {
            ObservableCollection<Delivery> deliveryData = new ObservableCollection<Delivery>();

            SqlConnection conn = CreateConnection();
            conn.Open();

            // Get courier id from employee id
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT CourierID FROM Couriers WHERE EmployeeID = @id";
            cmd.Parameters.AddWithValue("@id", employeeId);

            // Cant find a courier match
            SqlDataReader rdr = cmd.ExecuteReader();
            if (!rdr.Read() || rdr["CourierID"] == DBNull.Value)
            {
                MessageBox.Show("Employee is not a courier");
                return deliveryData;
            }
            int courierId = (int)rdr["CourierID"];
            rdr.Close();

            // Get the deliveries related to the courier
            SqlCommand cmd2 = conn.CreateCommand();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = "SELECT * FROM Deliveries WHERE CourierID = @id ";
            cmd2.Parameters.AddWithValue("@id", courierId);

            SqlDataReader rdr2 = cmd2.ExecuteReader();
            while (rdr2.Read())
            {
                Delivery newDelivery = new Delivery();
                newDelivery.id = (int)rdr2[0];
                newDelivery.destinationAddress = (string)rdr2["DestinationAddress"];
                newDelivery.deliveryDateTime = (DateTime)rdr2["DeliveryDateTime"];
                newDelivery.contractId = (int)rdr2["ContractID"];
                newDelivery.delivered = (int)rdr2["Delivered"];

                if (rdr2["CourierID"] == null || rdr2["CourierID"] == DBNull.Value) // Because courier can sometimes be null
                {
                    newDelivery.courierId = null;
                }
                else
                {
                    newDelivery.courierId = (int)rdr2["CourierID"];
                }

                newDelivery.accepted = (int)rdr2["Accepted"];
                deliveryData.Add(newDelivery);
            }

            conn.Close();
            return deliveryData;
        }

        public static ObservableCollection<Delivery> GetAllDeliveries()
        {
            ObservableCollection<Delivery> deliveryData = new ObservableCollection<Delivery>();

            SqlConnection conn = CreateConnection();
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM Deliveries";

            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Delivery newDelivery = new Delivery();
                newDelivery.id = (int)rdr[0];
                newDelivery.destinationAddress = (string)rdr["DestinationAddress"];
                newDelivery.deliveryDateTime = (DateTime)rdr["DeliveryDateTime"];
                newDelivery.contractId = (int)rdr["ContractID"];
                newDelivery.delivered = (int)rdr["Delivered"];

                if (rdr["CourierID"] == null || rdr["CourierID"] == DBNull.Value) // Because courier can sometimes be null
                {
                    newDelivery.courierId = null;
                }
                else
                {
                    newDelivery.courierId = (int)rdr["CourierID"];
                }

                newDelivery.accepted = (int)rdr["Accepted"];
                deliveryData.Add(newDelivery);
            }

            conn.Close();
            return deliveryData;
        }

        public static void DeleteDeliveryWithId(int id)
        {

            SqlConnection conn = CreateConnection();
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "DELETE FROM Deliveries WHERE DeliveryID = @id;";
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static void UpdateDeliveryWithId(int id, string destinationAddress, DateTime deliveryDate, int? courierId, int? contractId)
        {
            SqlConnection conn = CreateConnection();
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE Deliveries SET DestinationAddress = @destinationAddress, CourierID = @courierId, ContractID = @contractId, DeliveryDateTime = @deliveryDate WHERE DeliveryID = @id;";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@destinationAddress", destinationAddress);
            cmd.Parameters.AddWithValue("@courierId", courierId);
            cmd.Parameters.AddWithValue("@contractId", contractId);
            cmd.Parameters.AddWithValue("@deliveryDate", deliveryDate);

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static void AcceptDelivery(int deliveryId)
        {
            SqlConnection conn = CreateConnection();
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE Deliveries SET Accepted = 1 WHERE DeliveryID = @id";
            cmd.Parameters.AddWithValue("@id", deliveryId);

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static void DeclineDelivery(int deliveryId)
        {
            SqlConnection conn = CreateConnection();
            conn.Open();

            // Get courier ID for this delivery
            SqlCommand getCourierCmd = conn.CreateCommand();
            getCourierCmd.CommandType = CommandType.Text;
            getCourierCmd.CommandText = "SELECT CourierID FROM Deliveries WHERE DeliveryID = @id";
            getCourierCmd.Parameters.AddWithValue("@id", deliveryId);
            SqlDataReader courierResult = getCourierCmd.ExecuteReader();

            if (courierResult.Read() && courierResult[0] != DBNull.Value) // If found courier
            {
                int courierId = (int)courierResult[0];
                courierResult.Close();

                // Increment CancelledOrders
                SqlCommand updateCourierCmd = conn.CreateCommand();
                updateCourierCmd.CommandType = CommandType.Text;
                updateCourierCmd.CommandText = "UPDATE Couriers SET CancelledOrders = CancelledOrders + 1 WHERE CourierID = @courierId";
                updateCourierCmd.Parameters.AddWithValue("@courierId", courierId);
                updateCourierCmd.ExecuteNonQuery();
            }
            courierResult.Close();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE Deliveries SET CourierID = @null WHERE DeliveryID = @id";
            cmd.Parameters.AddWithValue("@id", deliveryId);
            cmd.Parameters.AddWithValue("@null", DBNull.Value);

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static void MarkDeliveryComplete(int deliveryId)
        {
            SqlConnection conn = CreateConnection();
            conn.Open();

            // Get courier ID for this delivery
            SqlCommand getCourierCmd = conn.CreateCommand();
            getCourierCmd.CommandType = CommandType.Text;
            getCourierCmd.CommandText = "SELECT CourierID FROM Deliveries WHERE DeliveryID = @id";
            getCourierCmd.Parameters.AddWithValue("@id", deliveryId);
            SqlDataReader courierResult = getCourierCmd.ExecuteReader();

            if (courierResult.Read() && courierResult[0] != DBNull.Value) // If found courier
            {
                int courierId = (int)courierResult[0];
                courierResult.Close();

                // Increment CancelledOrders
                SqlCommand updateCourierCmd = conn.CreateCommand();
                updateCourierCmd.CommandType = CommandType.Text;
                updateCourierCmd.CommandText = "UPDATE Couriers SET CompletedOrders = CompletedOrders + 1 WHERE CourierID = @courierId";
                updateCourierCmd.Parameters.AddWithValue("@courierId", courierId);
                updateCourierCmd.ExecuteNonQuery();
            }
            courierResult.Close();


            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE Deliveries SET Delivered = 1 WHERE DeliveryID = @id";
            cmd.Parameters.AddWithValue("@id", deliveryId);

            cmd.ExecuteNonQuery();

            conn.Close();
        }


        ///////////////////////
        /// Report DB Funcs ///
        ///////////////////////

        public static ObservableCollection<Delivery> GetCourierDeliveriesByDay(int courierId, DateTime date)
        {
            ObservableCollection<Delivery> deliveryData = new ObservableCollection<Delivery>();

            SqlConnection conn = CreateConnection();
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM Deliveries WHERE CourierID = @id AND CAST(DeliveryDateTime AS DATE) = @date";
            cmd.Parameters.AddWithValue("@id", courierId);
            cmd.Parameters.AddWithValue("@date", date.Date);

            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Delivery newDelivery = new Delivery();
                newDelivery.id = (int)rdr[0];
                newDelivery.destinationAddress = (string)rdr["DestinationAddress"];
                newDelivery.deliveryDateTime = (DateTime)rdr["DeliveryDateTime"];
                newDelivery.contractId = (int)rdr["ContractID"];
                newDelivery.delivered = (int)rdr["Delivered"];

                if (rdr["CourierID"] == null || rdr["CourierID"] == DBNull.Value) // Because courier can sometimes be null
                {
                    newDelivery.courierId = null;
                }
                else
                {
                    newDelivery.courierId = (int)rdr["CourierID"];
                }

                newDelivery.accepted = (int)rdr["Accepted"];
                deliveryData.Add(newDelivery);
            }

            return deliveryData;

        }

        public static ObservableCollection<Delivery> GetDeliveriesByMonth(DateTime date)
        {
            ObservableCollection<Delivery> deliveryData = new ObservableCollection<Delivery>();

            SqlConnection conn = CreateConnection();
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM Deliveries WHERE MONTH(DeliveryDateTime) = @month AND YEAR(DeliveryDateTime) = @year"; // Compare year and month
            cmd.Parameters.AddWithValue("@month", date.Month);
            cmd.Parameters.AddWithValue("@year", date.Year);

            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Delivery newDelivery = new Delivery();
                newDelivery.id = (int)rdr[0];
                newDelivery.destinationAddress = (string)rdr["DestinationAddress"];
                newDelivery.deliveryDateTime = (DateTime)rdr["DeliveryDateTime"];
                newDelivery.contractId = (int)rdr["ContractID"];
                newDelivery.delivered = (int)rdr["Delivered"];

                if (rdr["CourierID"] == null || rdr["CourierID"] == DBNull.Value) // Because courier can sometimes be null
                {
                    newDelivery.courierId = null;
                }
                else
                {
                    newDelivery.courierId = (int)rdr["CourierID"];
                }

                newDelivery.accepted = (int)rdr["Accepted"];
                deliveryData.Add(newDelivery);
            }

            return deliveryData;

        }

        public static (double activeContracts, double contractedDeliveries, double nonContractedDeliveries) GetMonthlyRevenueData(DateTime date)
        {
            int activeContracts = 0;
            int contractedDeliveries = 0;
            int nonContractedDeliveries = 0;

            DateTime startOfMonth = new DateTime(date.Year, date.Month, 1); // Provides dates to check if value is inbetween
            DateTime endOfMonth = startOfMonth.AddMonths(1);

            SqlConnection conn = CreateConnection();
            conn.Open();

            // Get the number of active Contracts
            SqlCommand cmd1 = conn.CreateCommand();
            cmd1.CommandText = @"
            SELECT COUNT(*) 
            FROM Contracts
            WHERE ContractStartDate < @endDate
            AND ContractEndDate >= @startDate";
            cmd1.Parameters.AddWithValue("@startDate", startOfMonth);
            cmd1.Parameters.AddWithValue("@endDate", endOfMonth);

            activeContracts = (int)cmd1.ExecuteScalar(); // Returns the first coloum (the count)

            // Contracted Deliveries
            SqlCommand cmd2 = conn.CreateCommand();
            cmd2.CommandText = @"
            SELECT COUNT(*) 
            FROM Deliveries
            WHERE (ContractID IS NOT NULL AND ContractID != 0)
            AND DeliveryDateTime >= @startDate
            AND DeliveryDateTime < @endDate";
            cmd2.Parameters.AddWithValue("@startDate", startOfMonth);
            cmd2.Parameters.AddWithValue("@endDate", endOfMonth);

            contractedDeliveries = (int)cmd2.ExecuteScalar();

            // Non-Contracted Deliveries
            SqlCommand cmd3 = conn.CreateCommand();
            cmd3.CommandText = @"
            SELECT COUNT(*) 
            FROM Deliveries
            WHERE (ContractID IS NULL OR ContractID = 0)
            AND DeliveryDateTime >= @startDate
            AND DeliveryDateTime < @endDate";
            cmd3.Parameters.AddWithValue("@startDate", startOfMonth);
            cmd3.Parameters.AddWithValue("@endDate", endOfMonth);

            nonContractedDeliveries = (int)cmd3.ExecuteScalar();

            conn.Close();

            return (activeContracts, contractedDeliveries, nonContractedDeliveries); // Returns tuple
        }
    }
}
