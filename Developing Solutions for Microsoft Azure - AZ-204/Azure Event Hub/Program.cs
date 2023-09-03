using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using EventHub;
using Newtonsoft.Json;
using System.Text;

string connectionstring = "Endpoint=sb://appevent.servicebus.windows.net/;SharedAccessKeyName=sendpolicy;SharedAccessKey=xPjXEAnJt+lQztR5wsI6hJRwHv4IPc6SORVFJVfiQn0=;EntityPath=apphub";
string eventHubName = "apphub";

var deviceList = new List<Device>()
    { new Device(){ DeviceId="D1",Temperature=100},
new Device(){ DeviceId="D2",Temperature=50},
new Device(){ DeviceId="D3",Temperature=90}};

await Sendata(deviceList);

async Task Sendata(List<Device> deviceList)
{
    var eventHubClient = new EventHubProducerClient(connectionstring, eventHubName);

    var eventBatch = await eventHubClient.CreateBatchAsync();

    foreach (var device in deviceList)
    {
        var eventData = new EventData(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(device)));

        if(!eventBatch.TryAdd(eventData))
        {
            Console.WriteLine("Error");
        }
    }
    await eventHubClient.SendAsync(eventBatch);

    Console.WriteLine("Events sent");

    await eventHubClient.DisposeAsync();
}