using Azure.Messaging.EventHubs.Consumer;
using System.Text;

string connectionstring = "Endpoint=sb://appevent.servicebus.windows.net/;SharedAccessKeyName=listenpolicy;SharedAccessKey=HVMfprgRl3r/BwQuMFVDeMLrFDHB3kepzZ4MFHJibQw=;EntityPath=apphub";
string consumerGroup = "$Default";

var partitionIds = await GetPartitionIds();

foreach (var partitionId in partitionIds)
{
    try
    {
        Console.WriteLine($"Getting event from partition: {partitionId}");
        await Readevents(partitionId);
    }
    catch (Exception)
    {

    }
}

async Task<string[]> GetPartitionIds()
{
    var partitionIds = new List<string>();
    var eventHubConsumerClient = new EventHubConsumerClient(consumerGroup, connectionstring);

    var partitionids = await eventHubConsumerClient.GetPartitionIdsAsync();

    foreach (var partitionid in partitionids)
    {
        partitionIds.Add(partitionid);
        Console.WriteLine($"Partitionid: {partitionid}");
    }

    return partitionIds.ToArray();
}

async Task Readevents(string partitionId)
{
    var eventHubConsumerClient = new EventHubConsumerClient(consumerGroup, connectionstring);

    //string partitionId = (await eventHubConsumerClient.GetPartitionIdsAsync()).First();

    var cancellationSource = new CancellationTokenSource();

    cancellationSource.CancelAfter(TimeSpan.FromSeconds(5));

    //var events = eventHubConsumerClient.ReadEventsAsync(cancellationSource.Token);

    var events = eventHubConsumerClient.ReadEventsFromPartitionAsync(partitionId, EventPosition.Earliest, cancellationSource.Token);

    await foreach (var evn in events)
    {
        Console.WriteLine($"Partitionid: {evn.Partition.PartitionId}");
        Console.WriteLine($"Offset: {evn.Data.Offset}");
        Console.WriteLine($"SequenceNumber: {evn.Data.SequenceNumber}");
        Console.WriteLine($"PartitionKey: {evn.Data.PartitionKey}");
        Console.WriteLine($"Data: {Encoding.UTF8.GetString(evn.Data.EventBody)}");
    }
}