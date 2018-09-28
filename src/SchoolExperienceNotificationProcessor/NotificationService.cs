using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly.Registry;
using SchoolExperienceEvents.Dto;

namespace SchoolExperienceNotificationProcessor
{
    internal class NotificationService : BackgroundService
    {
        public const string PolicyRegistryKey = "NotificationService";

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// The logger factory.
        /// </summary>
        private readonly ILoggerFactory _loggerFactory;

        /// <summary>
        /// The options.
        /// </summary>
        private readonly NotificationServiceOptions _options;

        /// <summary>
        /// The policy registry.
        /// </summary>
        private readonly IPolicyRegistry<string> _policyRegistry;

        /// <summary>
        /// The readers.
        /// </summary>
        private readonly IDictionary<string, NotificationQueueReader> _readers = new Dictionary<string, NotificationQueueReader>();

        /// <summary>
        /// The notify service.
        /// </summary>
        private readonly INotifyService _notifyService;

        /// <summary>
        /// The telemetry client.
        /// </summary>
        private readonly TelemetryClient _telemetryClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationService" /> class.
        /// </summary>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <param name="options">The options.</param>
        /// <param name="policyRegistry">The policy registry.</param>
        /// <param name="notifyService">The notify service.</param>
        /// <param name="telemetryClient">The telemetry client.</param>
        public NotificationService(
            ILoggerFactory loggerFactory,
            IOptions<NotificationServiceOptions> options,
            IPolicyRegistry<string> policyRegistry,
            INotifyService notifyService,
            TelemetryClient telemetryClient)
        {
            _logger = loggerFactory.CreateLogger(GetType());
            _loggerFactory = loggerFactory;
            _options = options.Value;
            _policyRegistry = policyRegistry;
            _notifyService = notifyService;
            _telemetryClient = telemetryClient;
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _readers.Add(QueueNames.Notification, new NotificationQueueReader(_options.QueueConnectionString, QueueNames.Notification, _policyRegistry, PolicyRegistryKey, _telemetryClient, _loggerFactory, cancellationToken, _notifyService));

            while (!cancellationToken.IsCancellationRequested)
            {
                foreach (var reader in _readers)
                {
                    try
                    {
                        reader.Value.ProcessMessagesAsync().ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Unable to process messages from: {reader.Key} - {ex.Message}");
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}
