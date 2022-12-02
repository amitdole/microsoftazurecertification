using System.Data.SqlClient;
using DemoApplication2.Models;

namespace DemoApplication2.Services
{
    public class ProductService : IProductService
    {

        public ProductService()
        {
        }

        private SqlConnection GetConnection()
        {
            var connectionstring = "Server=tcp:azuredevpractice.database.windows.net,1433;Initial Catalog=mysuperstore;Persist Security Info=False;User ID=mysuperstore;Password=Test@1234567;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            return new SqlConnection(connectionstring);
        }
        public async Task<List<Product>> GetProducts()
        {
            var list = new List<Product>();
            var statement = "SELECT ID, Name, Quantity FROM Products";
            var connection = GetConnection();
            connection.Open();

            var sqlCommand = new SqlCommand(statement, connection);

            using (var reader = sqlCommand.ExecuteReader())
            {
                while(reader.Read())
                {
                    var product = new Product
                    {
                        ID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Quantity = reader.GetInt32(2)
                    };

                    list.Add(product);
                }

                connection.Close();
                return list;
            }
        }
    }
}