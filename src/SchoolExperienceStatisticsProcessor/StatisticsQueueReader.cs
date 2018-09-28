using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Logging;
using Polly.Registry;
using SchoolExperienceEvents.Dto;
using SchoolExperienceProcessorShared;

namespace SchoolExperienceStatisticsProcessor
{
    internal class StatisticsQueueReader : EventProcessorBase
    {
        public StatisticsQueueReader(
            string queueName, 
            string connectionString, 
            IPolicyRegistry<string> policyRegistry, 
            string policyKey, 
            TelemetryClient telemetryClient, 
            ILoggerFactory loggerFactory, 
            CancellationToken cancellationToken) 
            : base(queueName, connectionString, policyRegistry, policyKey, telemetryClient, loggerFactory, cancellationToken)
        {
            RegisterEventHandler(EventNames.AddBooking, typeof(AddBookingEvent), m => AddBooking((AddBookingEvent)m));
        }

        Random random = new Random();

        private Task AddBooking(AddBookingEvent m)
        {
            if(random.Next(4) == 0)
            {
                throw new ArgumentException("Random error");
            }
            return Task.CompletedTask;
        }
    }
}
