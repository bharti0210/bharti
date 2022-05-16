namespace Asos.CodeTest
{
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;

    public class ArchivedDataService
    {
        private readonly string connectionString;

        public ArchivedDataService()
        {
            connectionString = ConfigurationManager.ConnectionStrings["Archive.Database.Connection"].ConnectionString;
        }

        public Customer GetArchivedCustomer(int customerId)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                var command = new SqlCommand("SELECT Id, Name FROM Customer WHERE CustomerId = @customerId", sqlConnection) { CommandType = CommandType.Text };
                command.Parameters.AddWithValue("@customerId", customerId);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Customer { Id = reader.GetInt32(0), Name = reader.GetString(1) };
                    }
                }
            }

            return null;
        }
    }
}