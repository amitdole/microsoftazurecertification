using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace PSAZDemo1FunctionsApp1
{
    public class Function1
    {
        //ConnectionStringSetting from functionapp -> Confirgurations -> AppSettings
        [FunctionName("VSLocationsFromQueue")]
        public void Run([QueueTrigger("locations", Connection = "psazdemo1storage")] string myQueueItem,
            [CosmosDB("PSAZDemo1CosmosDB", "psazdemo1cosmosdbcontainer",
            ConnectionStringSetting ="psazdemo1cosmosdbaccount_DOCUMENTDB")] out object locationDocument, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");

            dynamic location = JObject.Parse(myQueueItem);

            locationDocument = new
            {
                id = System.Guid.NewGuid().ToString(),
                country = location.country,
                Name = location.Name,
                Address = location.Address,
                Telephone = location.Telephone
            };
        }
    }
}
