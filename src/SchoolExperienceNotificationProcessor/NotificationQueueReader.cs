using System;
using System.Threading;
using Microsoft.ApplicationInsights;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly.Registry;
using SchoolExperienceEvents.Dto;
using SchoolExperienceNotificationProcessor.MessageProcessors;
using SchoolExperienceProcessorShared;

namespace SchoolExperienceNotificationProcessor
{
    public class NotificationQueueReader : EventProcessorBase
    {
        private readonly ILogger _logger;

        public NotificationQueueReader(
            string connectionString,
            string queueName,
            IPolicyRegistry<string> policyRegistry,
            string policyKey,
            TelemetryClient telemetryClient,
            ILoggerFactory loggerFactory,
            IServiceProvider serviceProvider,
            CancellationToken cancellationToken
            )
            : base(queueName, connectionString, policyRegistry, policyKey, telemetryClient, loggerFactory, serviceProvider, cancellationToken)
        {
            _logger = loggerFactory.CreateLogger(GetType());

            RegisterEventHandler(EventNames.AddBooking, typeof(AddBookingEvent), typeof(AddBookingMessageProcessor));
        }

        public static IServiceCollection AddMessageProcessors(IServiceCollection services)
        {
            services.AddScoped<AddBookingMessageProcessor>();

            return services;
        }
    }
}
