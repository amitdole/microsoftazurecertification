using System.Data.SqlClient;
using DemoApplication2.Models;
using MySql.Data.MySqlClient;

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

        private MySqlConnection GetMySqlConnection()
        {
            var connectionstring = "Server=azure400mysql.mysql.database.azure.com; Port=3306; Database=appdb; Uid=MyPSVMApp@azure400mysql; Pwd=mysqlS@123; SslMode=Preferred;";
            return new MySqlConnection(connectionstring);
        }

        public async Task<List<Product>> GetProducts()
        {
            var list = new List<Product>();
            var statement = "SELECT ID, Name, Quantity FROM Products";
            var connection = GetMySqlConnection();
            connection.Open();

            var sqlCommand = new MySqlCommand(statement, connection);

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

                connection.Clone();
                return list;
            }


            //var functionUrl = "https://azure400functionapp.azurewebsites.net/api/GetProducts?code=VeAUI2MmwdWz6lTv1JbE41AavPnP7k6T-MaWNOUmF3THAzFuqRUhEg==";
            
            //using(var client = new HttpClient())
            //{
            //    var response = await client.GetAsync(functionUrl);

            //    var content = await response.Content.ReadAsStringAsync();

            //    return JsonSerializer.Deserialize<List<Product>>(content);
            //}
        }
    }
}