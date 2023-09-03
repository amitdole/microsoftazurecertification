using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace AzureFunctionApp
{
    public static class AddProduct
    {
        [FunctionName("AddProduct")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var data = JsonConvert.DeserializeObject<Product>(requestBody);
            try
            {
                var connection = GetConnection();

                connection.Open();

                var statement = $"INSERT INTO Products (ID, Name, Quantity) VALUES ({data.Id},'{data.Name}',{data.Quantity})";
                var command = new SqlCommand(statement, connection);

                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw;
            }
            return new OkObjectResult($"Product {data.Id} has been added");
        }

        private static SqlConnection GetConnection()
        {
            string connectionString = Environment.GetEnvironmentVariable("SQLAZURECONNSTR_SqlConnectionString");
            return new SqlConnection(connectionString);
        }
    }
}
