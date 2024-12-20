using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FunctionApp1.triggers
{
    public class ServiceBusTrigger
    {
        private readonly ILogger<ServiceBusTrigger> _logger;

        public ServiceBusTrigger(ILogger<ServiceBusTrigger> logger)
        {
            _logger = logger;
        }

        [Function(nameof(ServiceBusTrigger))]
        public async Task Run(
            [ServiceBusTrigger("firstqueue", Connection = "ServiceBusConnectionString")]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            _logger.LogInformation("Message ID: {id}", message.MessageId);
            _logger.LogInformation("Message Body: {body}", message.Body);
            _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

            await messageActions.CompleteMessageAsync(message);

            // When an order is placed, the order details could be placed in the Service Bus, which triggers a function to process the order, charge the customer, and update the inventory.
        }
    }
}
