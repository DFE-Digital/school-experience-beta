using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Polly.Registry;
using SchoolExperienceEvents.AzureServices.Implementation;
using SchoolExperienceEvents.Dto;

namespace SchoolExperienceProcessorShared
{
    public abstract class EventProcessorBase : QueueReaderBase
    {
        private readonly IDictionary<string, MessageProcessorInfo> _eventProcessors = new Dictionary<string, MessageProcessorInfo>();
        private readonly ILogger _logger;
        private readonly TelemetryClient _telemetryClient;

        protected EventProcessorBase(
            string queueName,
            string connectionString,
            IPolicyRegistry<string> policyRegistry,
            string policyKey,
            TelemetryClient telemetryClient,
            ILoggerFactory loggerFactory,
            CancellationToken cancellationToken)
            : base(connectionString, policyRegistry, policyKey, cancellationToken)
        {
            _telemetryClient = telemetryClient;
            _logger = loggerFactory.CreateLogger(GetType());
            QueueName = queueName;
        }

        protected string QueueName { get; }

        public void RegisterEventHandler(string eventName, Type payloadType, Func<object, Task> processor)
        {
            _eventProcessors[eventName] = new MessageProcessorInfo
            {
                MessageType = payloadType,
                ProcessAsync = processor,
            };
        }

        public async Task ProcessMessagesAsync()
        {
            var messages = (await GetEvents<EventGridEvent>(QueueName, 10).ConfigureAwait(false)).ToList();

            if (messages.Any())
            {
                _logger.LogInformation($"Found {messages.Count} messages");

                foreach (var message in messages)
                {
                    await ProcessMessageAsync(message);
                }
            }
            else
            {
                await Task.Delay(TimeSpan.FromSeconds(5));
            }
        }

        private async Task ProcessMessageAsync(Microsoft.WindowsAzure.Storage.Queue.CloudQueueMessage message)
        {
            var telemetry = new DependencyTelemetry
            {
                Type = "Queue",
                Name = $"Dequeue {QueueName}",
            };

            telemetry.Start();

            try
            {
                var payload = JsonConvert.DeserializeObject<EventGridEvent>(message.AsString);

                if (_eventProcessors.TryGetValue(payload.Subject, out var processor))
                {
                    try
                    {
                        //telemetry.Context.Operation.Id = payload.RootId;
                        //telemetry.Context.Operation.ParentId = payload.ParentId;

                        if (payload.Data is JObject jObject)
                        {
                            _logger.LogInformation($"Message: {payload.Subject}");
                            var notificationEvent = (INotificationEvent)jObject.ToObject(processor.MessageType);
                            await processor.ProcessAsync(notificationEvent);
                            _telemetryClient.TrackEvent(payload.Subject);
                            await DeleteEvent(QueueName, message);
                        }
                        else
                        {
                            _logger.LogWarning($"Unknown message payload: {payload.Data}");
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning($"Message processing failed: {message.AsString}");
                        _telemetryClient.TrackException(ex);
                    }
                }
                else
                {
                    _logger.LogWarning($"Unknown message subject: {payload.Subject}");
                }
            }
            catch (StorageException ex)
            {
                telemetry.Properties.Add("AzureServiceRequestID", ex.RequestInformation.ServiceRequestID);
                telemetry.Success = false;
                telemetry.ResultCode = ex.RequestInformation.HttpStatusCode.ToString();
                _telemetryClient.TrackException(ex);
            }
            finally
            {
                // Update status code and success as appropriate.
                telemetry.Stop();
                _telemetryClient.Track(telemetry);
            }
        }

        private class MessageProcessorInfo
        {
            public Type MessageType { get; set; }
            public Func<INotificationEvent, Task> ProcessAsync { get; set; }
        }
    }
}
