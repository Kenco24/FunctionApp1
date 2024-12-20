using System;
using Azure.Messaging.EventHubs;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FunctionApp1.Triggers
{
    public class TemperatureMonitoringFunction
    {
        private readonly ILogger<TemperatureMonitoringFunction> _logger;

        public TemperatureMonitoringFunction(ILogger<TemperatureMonitoringFunction> logger)
        {
            _logger = logger;
        }

        [Function("TemperatureMonitoringFunction")]
        public void Run([EventHubTrigger("sensordata", Connection = "EventHubConnectionString")] EventData[] events)
        {
            foreach (EventData eventData in events)
            {
                try
                {
                    // Parse the event data
                    var eventBody = eventData.EventBody.ToString();
                    var sensorData = System.Text.Json.JsonSerializer.Deserialize<SensorData>(eventBody);

                    if (sensorData == null)
                    {
                        _logger.LogWarning("Invalid sensor data format.");
                        continue;
                    }

                    _logger.LogInformation($"Sensor ID: {sensorData.SensorId}, Temperature: {sensorData.Temperature}°C, Timestamp: {sensorData.Timestamp:MM/dd/yyyy HH:mm:ss}");

                    // Check temperature threshold
                    const double temperatureThreshold = 25.0; 
                    if (sensorData.Temperature > temperatureThreshold)
                    {
                        _logger.LogWarning($"ALERT: Sensor {sensorData.SensorId} reports high temperature: {sensorData.Temperature}°C.");
                        // simulate sending mail to subscribed users warning about temperature
                        SendAlert(sensorData);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error processing the event: {ex.Message}");
                }
            }
        }

        private void SendAlert(SensorData sensorData)
        {
            // Simulate sending an alert
            _logger.LogInformation($"Alert sent for Sensor {sensorData.SensorId}: Temperature {sensorData.Temperature}°C exceeds the threshold.");
            // Implement integration with email, SMS... 
        }
    }

    public class SensorData
    {
        public string SensorId { get; set; }
        public double Temperature { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
