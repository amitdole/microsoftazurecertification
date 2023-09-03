using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;

namespace AzureFunctionApp
{
    public static class GetProduct
    {
        [FunctionName("GetProducts")]
        public static async Task<IActionResult> RunProducts(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            var connection = GetConnection();

            var products = new List<Product>();

            connection.Open();

            var command = new SqlCommand("SELECT ID, Name, Quantity FROM Products", connection);

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (reader.Read())
                {
                    var product = new Product()
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Quantity = reader.GetInt32(2)
                    };

                    products.Add(product);
                }
            }

            connection.Close();
            return new OkObjectResult(JsonConvert.SerializeObject(products));
        }

        private static SqlConnection GetConnection()
        {
            string connectionString = Environment.GetEnvironmentVariable("SQLAZURECONNSTR_SqlConnectionString");
            return new SqlConnection(connectionString);
        }

        [FunctionName("GetProduct")]
        public static async Task<IActionResult> RunProduct(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            int productid = int.Parse(req.Query["id"]);

            var connection = GetConnection();

            var product = new Product();

            connection.Open();

            var command = new SqlCommand($"SELECT ID, Name, Quantity FROM Products WHERE ID = {productid}", connection);

            try
            {
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        product = new Product()
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Quantity = reader.GetInt32(2)
                        };
                    }
                }
                connection.Close();
                return new OkObjectResult(product);
            }
            catch (System.Exception)
            {
                connection.Close();
                return new NotFoundResult();
            }
        }
    }
}