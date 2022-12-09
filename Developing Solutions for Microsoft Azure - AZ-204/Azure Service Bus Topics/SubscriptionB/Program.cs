using Azure.Messaging.ServiceBus;
using Azure_Storage_Bus;
using Newtonsoft.Json;

string connectionstring = "Endpoint=sb://app400.servicebus.windows.net/;SharedAccessKeyName=listnenpolicy;SharedAccessKey=7jF1MkSwwFhKYYh473rNHgCuutV1BWax4cLSJA86ev8=;EntityPath=apptopic";
string topicname = "apptopic";
string subscriptionName = "SubscriptionB";

await ReceiveMessage();

async Task ReceiveMessage()
{
    var serviceBusClient = new ServiceBusClient(connectionstring);

    var receiver = serviceBusClient.CreateReceiver(topicname, subscriptionName,
        new ServiceBusReceiverOptions() { ReceiveMode = ServiceBusReceiveMode.PeekLock });

    IReadOnlyList<ServiceBusReceivedMessage> messages = await receiver.ReceiveMessagesAsync(1000);

    Console.WriteLine($"SubscriptionB");

    foreach (ServiceBusReceivedMessage message in messages)
    {
        var order = JsonConvert.DeserializeObject<Order>(message.Body.ToString());
        Console.WriteLine($"Order Id - {order.OrderId}");
        Console.WriteLine($"Quantity - {order.Quantity}");
    }
    Console.ReadLine();
    await receiver.DisposeAsync();
    await serviceBusClient.DisposeAsync();
}