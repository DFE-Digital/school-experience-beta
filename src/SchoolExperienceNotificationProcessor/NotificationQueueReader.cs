using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Notify.Client;
using Polly.Registry;
using SchoolExperienceEvents.AzureServices.Implementation;
using SchoolExperienceEvents.Dto;
using SchoolExperienceProcessorShared;

namespace SchoolExperienceNotificationProcessor
{
    public class NotificationQueueReader : EventProcessorBase
    {
        private const string WelcomeTemplate = "d2fa5b25-0348-4711-8c38-a154899d2ffa";
        private const string CandidateBookingTemplate = "0caea1ec-7562-4469-9c1f-c7f1fba3e4d0";

        private readonly ILogger _logger;

        private readonly INotifyService _notifyService;

        public NotificationQueueReader(
            string connectionString,
            string queueName,
            IPolicyRegistry<string> policyRegistry,
            string policyKey,
            TelemetryClient telemetryClient,
            ILoggerFactory loggerFactory,
            CancellationToken cancellationToken,
            INotifyService notifyService)
            : base(queueName, connectionString, policyRegistry, policyKey, telemetryClient, loggerFactory, cancellationToken)
        {
            _logger = loggerFactory.CreateLogger(GetType());
            _notifyService = notifyService;

            RegisterEventHandler(EventNames.AddBooking, typeof(AddBookingEvent), m => AddBookingAsync((AddBookingEvent)m));
        }

        private async Task AddBookingAsync(AddBookingEvent data)
        {
            var emailAddress = "neil.scales@transformuk.com";
            var personalisation = new Dictionary<string, object>
            {
                ["FirstName"] = data.CandidateName,
                ["SchoolName"] = data.SchoolName,
                ["BookingDate"] = data.When.ToLongDateString(),
                ["ConfirmUrl"] = "https://schoolexperiencebeta.azurewebsites.net/link/a9283b38"
            };

            await _notifyService.SendEmailAsync(emailAddress, CandidateBookingTemplate, personalisation);
        }

    }
}
