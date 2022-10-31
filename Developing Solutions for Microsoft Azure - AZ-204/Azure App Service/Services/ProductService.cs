using System.Data.SqlClient;
using DemoApplication2.Models;

namespace DemoApplication2.Services
{
    public class ProductService : IProductService
    {
        private readonly IConfiguration _config;

        public ProductService(IConfiguration config)
        {
            _config = config;
        }

        private SqlConnection GetConnection()
        {
            var connectionstring = _config.GetConnectionString("azuredevpractice");
            return new SqlConnection(connectionstring);
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