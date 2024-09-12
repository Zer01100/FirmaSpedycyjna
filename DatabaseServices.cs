using Microsoft.Data.SqlClient;

namespace FirmaSpedycyjna
{
    public class DatabaseService
    {
        private readonly string _connectionString;
        private Page _page;
        public DatabaseService(Page page)
        {
            _connectionString = "Server=ZER0\\SQLSERVER;Database=FirmaSpedycyjna;Integrated Security=True;TrustServerCertificate=True;";
            _page = page;
        }

        public string[] ExecuteSelectQuery(string query)
        {
            var results = new List<string>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var values = new List<string>();

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            if (reader.IsDBNull(i))
                            {
                                values.Add(string.Empty);
                            }
                            else
                            {
                                values.Add(reader[i].ToString());
                            }
                        }

                        results.Add(string.Join("\t", values));
                    }
                }
            }

            return results.ToArray();
        }

        public async void ExecuteGeneralQuery(string query)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    await _page.DisplayAlert("Błąd", ex.Message, "OK");
                }
            }
        }
    }
}
