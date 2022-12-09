using System;
using System.Text;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace ServiceBusQueueFunctionApp
{
    public class AppQueue
    {
        [FunctionName("GetMessages")]
        public static void Run([ServiceBusTrigger("appqueue", Connection = "connectionstring")]Message queueMessage, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {Encoding.UTF8.GetString(queueMessage.Body)}");
            log.LogInformation($"C# MessageId: {queueMessage.MessageId}");
            log.LogInformation($"C# SequenceNumber: {queueMessage.SystemProperties.SequenceNumber}");
        }
    }
}
