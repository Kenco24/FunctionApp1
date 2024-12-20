using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FunctionApp1.Triggers
{
    public class TimerTriggerFunction
    {
        private readonly ILogger<TimerTriggerFunction> _logger;

        public TimerTriggerFunction(ILogger<TimerTriggerFunction> logger)
        {
            _logger = logger;
        }

        [Function(nameof(TimerTriggerFunction))]
        public void Run([TimerTrigger("*/30 * * * * *")] TimerInfo timer)
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            if (timer.IsPastDue)
            {
                _logger.LogWarning("The timer is running late!");
            }

            _logger.LogInformation("Timer trigger function logic executed.");

            //The timer trigger could be used to run a cleanup function every night at midnight to delete outdated records in a database, ensuring the database stays lean and efficient.

        }


    }
}
