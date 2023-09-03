using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Processor;
using Azure.Storage.Blobs;

string connectionstring = "Endpoint=sb://appevent.servicebus.windows.net/;SharedAccessKeyName=listenpolicy;SharedAccessKey=HVMfprgRl3r/BwQuMFVDeMLrFDHB3kepzZ4MFHJibQw=;EntityPath=apphub";
string consumerGroup = "$Default";
string blobConnectionString = "DefaultEndpointsProtocol=https;AccountName=azure400storagegroup;AccountKey=0TD8ND+dOTOnw3r6R7E3pVyzWo2/np50zifGmD5Jw836p8x2aLRe/+colXOWMg84N+ri6TGFlvtL+AStkem9rQ==;EndpointSuffix=core.windows.net";
string containerName = "checkpoint";

var bloblContainerClient = new BlobContainerClient(blobConnectionString, containerName);

var eventProcessorClient = new EventProcessorClient(bloblContainerClient, consumerGroup, connectionstring); ;

eventProcessorClient.ProcessEventAsync += ProcessEvents;
eventProcessorClient.ProcessErrorAsync += ErrorHandler;

Console.WriteLine("Started Listening");

await eventProcessorClient.StartProcessingAsync();

Console.ReadLine();

await eventProcessorClient.StopProcessingAsync();

async Task ProcessEvents(ProcessEventArgs processEvent)
{
    Console.WriteLine(processEvent.Data.EventBody.ToString());
}

static Task ErrorHandler(ProcessErrorEventArgs errorEvent)
{
    Console.WriteLine(errorEvent.Exception.ToString);

    return Task.CompletedTask;
}