using Azure;
using Azure.Data.Tables;

string connectionString = "DefaultEndpointsProtocol=https;AccountName=azure400storagegroup;AccountKey=0TD8ND+dOTOnw3r6R7E3pVyzWo2/np50zifGmD5Jw836p8x2aLRe/+colXOWMg84N+ri6TGFlvtL+AStkem9rQ==;EndpointSuffix=core.windows.net";
string tableName = "Orders";

//AddEntity("O1", "Mobile", 100);
//AddEntity("O2", "Mobile", 50);
//AddEntity("O3", "Laptop", 30);
//AddEntity("O4", "Tab", 100);
//GetEntity("Mobile");
DeleteEntity("Mobile", "O1");
Console.ReadLine();

void DeleteEntity(string category, string OrderId)
{
    var tableClient = new TableClient(connectionString, tableName);
    tableClient.DeleteEntity(category, OrderId);

    Console.WriteLine($"Deleted : {OrderId}");
}

void GetEntity(string category)
{
    var tableClient = new TableClient(connectionString, tableName);

    Pageable<TableEntity> results = tableClient.Query<TableEntity>(entity=> entity.PartitionKey == category);

    foreach (var item in results)
    {
        Console.WriteLine($"OrderID : {item.RowKey}, Category: {item.RowKey}, Quantity: {item.GetInt32("quantity")}");
    }
}

void AddEntity(string orderId, string category, int quantity)
{
    var tableClient = new TableClient(connectionString, tableName);

    var tableEntity = new TableEntity(category, orderId)
    {
        { "quantity", quantity}
    };

    tableClient.AddEntity(tableEntity);

    Console.WriteLine($"Added Entity : {orderId}");
}