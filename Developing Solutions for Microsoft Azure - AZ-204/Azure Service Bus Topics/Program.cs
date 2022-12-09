using Azure.Messaging.ServiceBus;
using Azure_Storage_Bus;
using Newtonsoft.Json;

string connectionstring = "Endpoint=sb://app400.servicebus.windows.net/;SharedAccessKeyName=sendpolicy;SharedAccessKey=M+R1rJbXh77Zu9J8j9ibMoeBFOQnHel6k6TYEcbr3mI=;EntityPath=apptopic";
string topicname = "apptopic";

var orders = new List<Order>()
{
     new Order(){OrderId = "O1", Quantity=100},
     new Order(){OrderId = "O2", Quantity=2}
};

await SendMessage(orders);

async Task SendMessage(List<Order> orders)
{

    await using var serviceBusClient = new ServiceBusClient(connectionstring);

    var serviceBusSender = serviceBusClient.CreateSender(topicname);

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