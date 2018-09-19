using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Polly.Registry;
using SchoolExperienceEvents.AzureServices.Implementation;
using SchoolExperienceEvents.Dto;

namespace SchoolExperienceNotificationProcessor
{
    public class NotificationQueueReader : QueueReaderBase
    {
        private readonly IDictionary<string, MessageProcessor> _eventProcessors = new Dictionary<string, MessageProcessor>();
        private readonly ILogger _logger;
        private readonly string _queueName;
        public NotificationQueueReader(
            string connectionString,
            string queueName,
            IPolicyRegistry<string> policyRegistry,
            string policyKey,
            ILoggerFactory loggerFactory,
            CancellationToken cancellationToken)
            : base(connectionString, policyRegistry, policyKey, cancellationToken)
        {
            _logger = loggerFactory.CreateLogger(GetType());

            _queueName = queueName;
            _eventProcessors[EventNames.AddBooking] = new MessageProcessor
            {
                MessageType = typeof(AddBookingEvent),
                ProcessorAsync = m => AddBookingAsync((AddBookingEvent)m)
            };
        }

        public async Task ProcessMessages()
        {
            var messages = (await GetEvents<EventGridEvent>(_queueName).ConfigureAwait(false)).ToList();

            if (messages.Any())
            {
                _logger.LogInformation($"Found {messages.Count} messages");

                foreach (var message in messages)
                {
                    var data = JsonConvert.DeserializeObject<EventGridEvent>(message.AsString);

                    if (_eventProcessors.TryGetValue(data.Subject, out var processor))
                    {
                        try
                        {
                            if (data.Data is JObject jObject)
                            {
                                _logger.LogInformation($"Message: {data.Subject}");
                                var notificationEvent = (INotificationEvent)jObject.ToObject(processor.MessageType);
                                await processor.ProcessorAsync(notificationEvent);
                                await DeleteEvent(_queueName, message);
                            }
                            else
                            {
                                _logger.LogWarning($"Unknown message payload: {data.Data}");
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogWarning($"Message processing failed: {message.AsString}");
                        }
                    }
                    else
                    {
                        _logger.LogWarning($"Unknown message subject: {data.Subject}");
                    }
                }
            }
        }

        private Task AddBookingAsync(AddBookingEvent data)
        {
            return Task.CompletedTask;
        }

        private class MessageProcessor
        {
            public Type MessageType { get; set; }
            public Func<INotificationEvent, Task> ProcessorAsync { get; set; }
        }
    }
}
