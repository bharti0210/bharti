namespace Asos.CodeTest
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;

    public class FailoverRepository
    {
        public List<FailoverEntry> GetFailOverEntries()
        {
            var failoverEntries = new List<FailoverEntry>();

            var connectionString = ConfigurationManager.ConnectionStrings["FailoverDatabase.Connection"].ConnectionString;

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                var command = new SqlCommand("GetFailoverEntries", sqlConnection) { CommandType = CommandType.StoredProcedure };

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var failoverData = new FailoverEntry { DateTime = reader.GetDateTime(0) };

                        failoverEntries.Add(failoverData);
                    }
                }
            }

            return failoverEntries;
        }
    }
}