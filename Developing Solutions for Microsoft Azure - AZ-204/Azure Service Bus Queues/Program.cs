using Azure.Messaging.ServiceBus;
using Azure_Storage_Bus;
using Newtonsoft.Json;

string connectionstring = "Endpoint=sb://app400.servicebus.windows.net/;SharedAccessKeyName=appqueuepolicy;SharedAccessKey=Ui8fIOMAY282NZ2pTDQNebAx2j8AG7KK2TQExkip1t8=;EntityPath=appqueue";
string queuename = "appqueue";

var orders = new List<Order>()
{
     new Order(){OrderId = "O1", Quantity=100},
     new Order(){OrderId = "O2", Quantity=2}
};

await SendMessage(orders);
//await PeekMessage();

////serviceBusProcessor
//await using var serviceBusClient = new ServiceBusClient(connectionstring);

//var serviceBusProcessor = serviceBusClient.CreateProcessor(queuename, new ServiceBusProcessorOptions());
//serviceBusProcessor.ProcessMessageAsync += ProcessMessage;
//serviceBusProcessor.ProcessErrorAsync += ErrorHandler;

//await serviceBusProcessor.StartProcessingAsync();
//Console.WriteLine($"Listening to messages");
//Console.ReadKey();

//await serviceBusProcessor.StopProcessingAsync();

//await serviceBusProcessor.DisposeAsync();
//await serviceBusClient.DisposeAsync();


static async Task ProcessMessage(ProcessMessageEventArgs messageEvent)
{
    var order = JsonConvert.DeserializeObject<Order>(messageEvent.Message.Body.ToString());
    Console.WriteLine($"Order Id - {order.OrderId}");
    Console.WriteLine($"Quantity - {order.Quantity}");
}

static async Task<Task> ErrorHandler(ProcessErrorEventArgs args)
{
    Console.WriteLine(args.Exception.ToString());
    return Task.CompletedTask;
}

async Task SendMessage(List<Order> orders)
{

    await using var serviceBusClient = new ServiceBusClient(connectionstring);

    var serviceBusSender = serviceBusClient.CreateSender(queuename);

    try
    {
        using var serviceBusMessageBatch = await serviceBusSender.CreateMessageBatchAsync();

        foreach (var order in orders)
        {
            var message = new ServiceBusMessage(JsonConvert.SerializeObject(order));
            message.ContentType = "application/json";
            message.ApplicationProperties.Add("priority", "High");

            message.TimeToLive = TimeSpan.FromSeconds(15);
            if (!serviceBusMessageBatch.TryAddMessage(message))
            {
                throw new Exception("Error occurred");
            }
        }

        Console.WriteLine("Sending Message");

        await serviceBusSender.SendMessagesAsync(serviceBusMessageBatch);   

        Console.WriteLine("Message Sent");

        Console.ReadLine();

        await serviceBusSender.DisposeAsync();
        await serviceBusClient.DisposeAsync();
    }
    catch (Exception e)
    {
    }
}