using Azure.Messaging.ServiceBus;
using Azure_Storage_Bus;
using Newtonsoft.Json;

string connectionstring = "Endpoint=sb://app400.servicebus.windows.net/;SharedAccessKeyName=appqueuepolicy;SharedAccessKey=Ui8fIOMAY282NZ2pTDQNebAx2j8AG7KK2TQExkip1t8=;EntityPath=appqueue";
string queuename = "appqueue";


await PeekMessage();
    
async Task PeekMessage()
{
    await using var serviceBusClient = new ServiceBusClient(connectionstring);

    var receiver = serviceBusClient.CreateReceiver(queuename,
        new ServiceBusReceiverOptions() { ReceiveMode = ServiceBusReceiveMode.PeekLock });

    var messages = receiver.ReceiveMessagesAsync();

    await foreach (var message in messages)
    {
        var order = JsonConvert.DeserializeObject<Order>(message.Body.ToString());
        Console.WriteLine($"Order Id - {order.OrderId}");
        Console.WriteLine($"Quantity - {order.Quantity}");

        await receiver.CompleteMessageAsync(message);
    }

    Console.ReadLine();
    await receiver.DisposeAsync();
    await serviceBusClient.DisposeAsync();
}