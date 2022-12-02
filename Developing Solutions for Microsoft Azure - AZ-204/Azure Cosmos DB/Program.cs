using Microsoft.Azure.Cosmos;
using Azure_Cosmos_DB;

string cosmosEndpoint = "https://cosmosdb.documents.azure.com:443/";
string cosmosKey = "";
//await CreateDatabase("appdb");
//await CreateContainer("appdb","Orders","/category");
//await AddItem("appdb", "Orders", new Order { OrderId = "O1", Category = "Laptop", Quantity = 100 });
//await AddItem("appdb", "Orders", new Order { OrderId = "O2", Category = "Laptop", Quantity = 30 });
//await AddItem("appdb", "Orders", new Order { OrderId = "O3", Category = "Mobile", Quantity = 200 });
//await ReadItem("appdb", "Orders");
//await ReplaceItem("appdb", "Orders", "O1");
//await DeleteItem("appdb", "Orders", "O1");
//await AddArrayItem("appdb", "Customers",
//    new Customer
//    {
//        CustomerId = "C1",
//        Name = "Test",
//        City = "London",
//        Orders = new List<Order>()
//                                {
//                                  new Order
//                                     {
//                                        OrderId = "O1",
//                                        Category="LapTop",
//                                        Quantity = 100
//                                    },
//                                   new Order
//                                    {
//                                        OrderId = "O2",
//                                        Category="LapTop",
//                                        Quantity = 120
//                                    }
//                                }
//    });

//await CallStoredProcedure("appdb", "Customers");

//await InsertStoredProcedure("appdb", "Customers");

await AddItemTrigger("appdb", "Customers", new Customer
{
    CustomerId = "C1",
    City = "London"
});

Console.ReadLine();

async Task InsertStoredProcedure(string databaseName, string containerName)
{
    var cosmosClient = new CosmosClient(cosmosEndpoint, cosmosKey);
    var container = cosmosClient.GetContainer(databaseName, containerName);

    dynamic[] customers = new dynamic[]
    {
        new
        {
            CustomerId = "C1",
            Name = "Test",
            city = "Paris",
        }
    };

    var response = await container.Scripts.ExecuteStoredProcedureAsync<string>("CreateItems", new PartitionKey("Paris"), new[] { customers });

    Console.WriteLine(response);

}

async Task CallStoredProcedure(string databaseName, string containerName)
{
    var cosmosClient = new CosmosClient(cosmosEndpoint, cosmosKey);
    var container = cosmosClient.GetContainer(databaseName, containerName);

    var response = await container.Scripts.ExecuteStoredProcedureAsync<string>("Display", new PartitionKey(""), null);

    Console.WriteLine(response);

}

async Task AddArrayItem(string databaseName, string containerName, Customer customer)
{
    var cosmosClient = new CosmosClient(cosmosEndpoint, cosmosKey);

    var database = cosmosClient.GetDatabase(databaseName);
    var container = database.GetContainer(containerName);

    var response = await container.CreateItemAsync<Customer>(customer, new PartitionKey(customer.City));
    Console.WriteLine($"Added Item with Order Id: {customer.CustomerId}, request charge: {response.RequestCharge}");
}


async Task DeleteItem(string databaseName, string containerName, string OrderId)
{
    var cosmosClient = new CosmosClient(cosmosEndpoint, cosmosKey);

    var database = cosmosClient.GetDatabase(databaseName);
    var container = database.GetContainer(containerName);

    var query = $"SELECT o.id, o.category, o.Quantity FROM Orders o WHERe o.id = '{OrderId}'";
    var queryDefination = new QueryDefinition(query);

    string orderId = "";
    string category = "";

    var fieldIterator = container.GetItemQueryIterator<Order>(queryDefination);

    while (fieldIterator.HasMoreResults)
    {
        var response = await fieldIterator.ReadNextAsync();
        foreach (var data in response)
        {
            orderId = data.OrderId;
            category = data.Category;
        }
    }

    var result = await container.DeleteItemAsync<Order>(orderId, new PartitionKey(category));

    Console.WriteLine($"Item is Deleted");
}


async Task ReplaceItem(string databaseName, string containerName, string OrderId)
{
    var cosmosClient = new CosmosClient(cosmosEndpoint, cosmosKey);

    var database = cosmosClient.GetDatabase(databaseName);
    var container = database.GetContainer(containerName);

    var query = $"SELECT o.id, o.category, o.Quantity FROM Orders o WHERe o.id = '{OrderId}'";
    var queryDefination = new QueryDefinition(query);

    string orderId = "";
    string category = "";

    var fieldIterator = container.GetItemQueryIterator<Order>(queryDefination);

    while (fieldIterator.HasMoreResults)
    {
        var response = await fieldIterator.ReadNextAsync();
        foreach (var data in response)
        {
            orderId = data.OrderId;
            category = data.Category;
        }
    }

    var result = await container.ReadItemAsync<Order>(orderId, new PartitionKey(category));

    var item = result.Resource;

    item.Quantity = 222;

    await container.ReplaceItemAsync<Order>(item, orderId, new PartitionKey(category));

    Console.WriteLine($"Item is updated");
}

async Task ReadItem(string databaseName, string containerName)
{
    var cosmosClient = new CosmosClient(cosmosEndpoint, cosmosKey);

    var database = cosmosClient.GetDatabase(databaseName);
    var container = database.GetContainer(containerName);

    var query = "SELECT o.OrderId, o.Category, o.Quantity FROM Orders o";
    var queryDefination = new QueryDefinition(query);

    var fieldIterator = container.GetItemQueryIterator<Order>(queryDefination);

    while (fieldIterator.HasMoreResults)
    {
        var response = await fieldIterator.ReadNextAsync();
        foreach (var item in response)
        {
            Console.WriteLine($"Order Id: {item.OrderId}, category: {item.Category}, quantity: {item.Quantity}");
        }
    }
}

async Task AddItemTrigger(string databaseName, string containerName, Customer customer)
{
    var cosmosClient = new CosmosClient(cosmosEndpoint, cosmosKey);

    var database = cosmosClient.GetDatabase(databaseName);
    var container = database.GetContainer(containerName);

    var response = await container.CreateItemAsync<Customer>(customer, new PartitionKey(customer.City), new ItemRequestOptions { PreTriggers = new List<string> { "ValidateItem" } });
    Console.WriteLine($"Added Item with Order Id: {customer.CustomerId}, request charge: {response.RequestCharge}");
}

async Task AddItem(string databaseName, string containerName, Order order)
{
    var cosmosClient = new CosmosClient(cosmosEndpoint, cosmosKey);

    var database = cosmosClient.GetDatabase(databaseName);
    var container = database.GetContainer(containerName);

    var response = await container.CreateItemAsync<Order>(order, new PartitionKey(order.Category));
    Console.WriteLine($"Added Item with Order Id: {order.OrderId}, request charge: {response.RequestCharge}");
}

async Task CreateDatabase(string databaseName)
{
    var cosmosClient = new CosmosClient(cosmosEndpoint, cosmosKey);

    await cosmosClient.CreateDatabaseAsync(databaseName);
    Console.WriteLine($"DB Created {databaseName}");
}


async Task CreateContainer(string databaseName, string containerName, string partitionKey)
{
    var cosmosClient = new CosmosClient(cosmosEndpoint, cosmosKey);

    var database = cosmosClient.GetDatabase(databaseName);

    await database.CreateContainerAsync(containerName, partitionKey);
    Console.WriteLine($"Container Created {containerName}");
}