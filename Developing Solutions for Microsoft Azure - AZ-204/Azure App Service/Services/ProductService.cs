using System.Data.SqlClient;
using DemoApplication2.Models;

namespace DemoApplication2.Services
{
    public class ProductService
    {
        private static string db_source = "azuredevpractice.database.windows.net";
        private static string db_user = "azuredevpractice";
        private static string db_password = "azuresqldb@123";
        private static string db_database = "azuredevpracticeDB";

        private SqlConnection GetConnection()
        {
            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = db_source;
            builder.UserID = db_user;
            builder.Password = db_password;
            builder.InitialCatalog = db_database;

            return new SqlConnection(builder.ConnectionString);
        }

        public List<Product> GetProducts()
        {
            var connection = GetConnection();

            var products = new List<Product>();

            string query = "SELECT * FROM Products";

            connection.Open();

            var command = new SqlCommand(query, connection);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var product = new Product
                    {
                        ProductID = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Quantity = reader.GetInt32(2),
                    };

                    products.Add(product);
                }
            }

            connection.Close();

            return products;
        }
    }
}