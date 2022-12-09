using Azure.Storage.Queues;
using Azure_Storage_Queues;
using Newtonsoft.Json;

string connectionstring = "DefaultEndpointsProtocol=https;AccountName=azure400storagegroup;AccountKey=0TD8ND+dOTOnw3r6R7E3pVyzWo2/np50zifGmD5Jw836p8x2aLRe/+colXOWMg84N+ri6TGFlvtL+AStkem9rQ==;EndpointSuffix=core.windows.net";
string queueName = "appqueue";

//SendMessage("This is 1st message");
//SendMessage("This is 2nd message");

//PeekMessage();
//ReceiveMessage();
//var queueLength = QueueLength();

//Console.WriteLine($"No of messages in queue: {queueLength}");

SendObjectMessage(new Order { Id = 1, Quantity = 100 });
SendObjectMessage(new Order { Id = 2, Quantity = 5 });
//PeekObjectMessage();
//ReceiveObjectMessage();

void SendObjectMessage(Order order)
{
    var queueClient = new QueueClient(connectionstring, queueName);

    if (queueClient.Exists())
    {
        var obj = JsonConvert.SerializeObject(order);
        var bytes = System.Text.Encoding.UTF8.GetBytes(obj);
        var message = System.Convert.ToBase64String(bytes);
        queueClient.SendMessage(message);

        Console.WriteLine($"Message was sent for Order -  {order.Id}");
    }
}

void PeekObjectMessage()
{
    var queueClient = new QueueClient(connectionstring, queueName);

    int maxMessage = 10;

    if (queueClient.Exists())
    {
        var peekMessages = queueClient.PeekMessages(maxMessage);

        foreach (var peekMessage in peekMessages.Value)
        {
            var order = JsonConvert.DeserializeObject<Order>(peekMessage.Body.ToString());
            Console.WriteLine($"Peeked message: {order.Id.ToString()} -  {order.Quantity.ToString()}");
        }
    }
}

void ReceiveObjectMessage()
{
    var queueClient = new QueueClient(connectionstring, queueName);

    int maxMessage = 10;

    if (queueClient.Exists())
    {
        var messages = queueClient.ReceiveMessages(maxMessage);

        foreach (var message in messages.Value)
        {
            var order = JsonConvert.DeserializeObject<Order>(message.Body.ToString());
            Console.WriteLine($"Received message: {order.Id.ToString()} -  {order.Quantity.ToString()}");

            queueClient.DeleteMessage(message.MessageId, message.PopReceipt);
        }
    }
}



void SendMessage(string message)
{
    var queueClient = new QueueClient(connectionstring, queueName);

    if(queueClient.Exists())
    {
        queueClient.SendMessage(message);

        Console.WriteLine($"Message was sent {message}");
    }
}

void PeekMessage()
{
    var queueClient = new QueueClient(connectionstring, queueName);

    int maxMessage = 10;

    if (queueClient.Exists())
    {
        var peekMessages = queueClient.PeekMessages(maxMessage);

        foreach (var peekMessage in peekMessages.Value)
        {
            Console.WriteLine($"Peeked message: {peekMessage.Body}");
        }
    }
}

void ReceiveMessage()
{
    var queueClient = new QueueClient(connectionstring, queueName);

    int maxMessage = 10;

    if (queueClient.Exists())
    {
        var messages = queueClient.ReceiveMessages(maxMessage);

        foreach (var message in messages.Value)
        {
            Console.WriteLine($"Received message: {message.Body}");
            queueClient.DeleteMessage(message.MessageId,message.PopReceipt);
        }
    }
}

int QueueLength()
{
    var queueClient = new QueueClient(connectionstring, queueName);

    if (queueClient.Exists())
    {
        var properties = queueClient.GetProperties();

        return properties.Value.ApproximateMessagesCount;
    }

    return 0;
}