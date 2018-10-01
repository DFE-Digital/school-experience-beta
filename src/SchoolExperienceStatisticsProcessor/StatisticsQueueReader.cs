using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly.Registry;
using SchoolExperienceEvents.Dto;
using SchoolExperienceProcessorShared;
using SchoolExperienceStatisticsProcessor.MessageProcessors;

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
            IServiceProvider serviceProvider,
            CancellationToken cancellationToken) 
            : base(queueName, connectionString, policyRegistry, policyKey, telemetryClient, loggerFactory, serviceProvider, cancellationToken)
        {
            RegisterEventHandler(EventNames.AddBooking, typeof(AddBookingEvent), typeof(AddBookingMessageProcessor));
        }

        public static IServiceCollection AddMessageProcessors(IServiceCollection services)
        {
            services.AddScoped<AddBookingMessageProcessor>();

            return services;
        }

    }
}
