using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FunctionApp1.Triggers
{
    public class QueueTriggerFunction
    {
        private readonly ILogger<QueueTriggerFunction> _logger;

        public QueueTriggerFunction(ILogger<QueueTriggerFunction> logger)
        {
            _logger = logger;
        }

        [Function(nameof(QueueTriggerFunction))]
        public void Run([QueueTrigger("messagesqueue", Connection = "funcbenefitsstorage")] QueueMessage message)
        {
            _logger.LogInformation($"C# Queue trigger function processed: {message.MessageText}");

            // When a user registers on a website, a confirmation email is added to a queue.
            // The queue trigger processes the email and sends it in the background, freeing up the web server for other tasks.
        }
    }
}
