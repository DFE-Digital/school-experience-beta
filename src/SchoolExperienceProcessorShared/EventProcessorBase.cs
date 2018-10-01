using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Extensions.DependencyInjection;
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
        private readonly IServiceProvider _serviceProvider;

        protected EventProcessorBase(
            string queueName,
            string connectionString,
            IPolicyRegistry<string> policyRegistry,
            string policyKey,
            TelemetryClient telemetryClient,
            ILoggerFactory loggerFactory,
            IServiceProvider serviceProvider,
            CancellationToken cancellationToken)
            : base(connectionString, policyRegistry, policyKey, cancellationToken)
        {
            _telemetryClient = telemetryClient;
            _logger = loggerFactory.CreateLogger(GetType());
            _serviceProvider = serviceProvider;
            QueueName = queueName;
        }

        protected string QueueName { get; }

        public void RegisterEventHandler(string eventName, Type payloadType, Type messageProcessorType)
        {
            _eventProcessors[eventName] = new MessageProcessorInfo
            {
                PayloadType = payloadType,
                MessageProcessorType = messageProcessorType,
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
                            using (var scope = _serviceProvider.CreateScope())
                            using (var instance = (IMessageProcessor)scope.ServiceProvider.GetRequiredService(processor.MessageProcessorType))
                            {
                                var notificationEvent = (INotificationEvent)jObject.ToObject(processor.PayloadType);
                                await instance.ProcessAsync(notificationEvent);
                                _telemetryClient.TrackEvent(payload.Subject);
                                await DeleteEvent(QueueName, message);
                            }
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
            public Type PayloadType { get; set; }
            public Type MessageProcessorType { get; set; }
        }
    }
}
