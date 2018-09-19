using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;

namespace SchoolExperienceEvents.AzureServices.Implementation
{
    internal abstract class EventGridSenderBase
    {
        private readonly EventGridClient _client;

        private readonly string _topicHostname;

        protected EventGridSenderBase(string topicEndpoint, string topicKey)
        {
            _topicHostname = new Uri(topicEndpoint).Host;
            var topicCredentials = new TopicCredentials(topicKey);
            _client = new EventGridClient(topicCredentials);
        }

        public async Task SendAsync<T>(string eventName, T data)
        {
            var events = new List<EventGridEvent>
            {
                new EventGridEvent()
                {
                    Id = Guid.NewGuid().ToString(),
                    EventType = typeof(T).Name,
                    Data = data,
                    EventTime = DateTime.UtcNow,
                    Subject = eventName,
                    DataVersion = "2.0"
                }
            };

            await _client.PublishEventsAsync(_topicHostname, events);
        }
    }
}
