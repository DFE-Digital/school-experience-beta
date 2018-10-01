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

namespace SchoolExperienceStatisticsProcessor
{
    internal class StatisticsService : BackgroundService
    {
        public const string PolicyRegistryKey = "StatisticsService";

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
        private readonly StatisticsServiceOptions _options;

        /// <summary>
        /// The policy registry.
        /// </summary>
        private readonly IPolicyRegistry<string> _policyRegistry;

        /// <summary>
        /// The readers.
        /// </summary>
        private readonly IDictionary<string, StatisticsQueueReader> _readers = new Dictionary<string, StatisticsQueueReader>();

        /// <summary>
        /// The telemetry client.
        /// </summary>
        private readonly TelemetryClient _telemetryClient;

        private StatisticsQueueReader _queueReader;
        IServiceProvider _serviceProvider;

        public StatisticsService(
            ILoggerFactory loggerFactory,
            IOptions<StatisticsServiceOptions> options,
            IPolicyRegistry<string> policyRegistry,
            IServiceProvider serviceProvider,
            TelemetryClient telemetryClient)
        {
            _logger = loggerFactory.CreateLogger(GetType());
            _loggerFactory = loggerFactory;
            _options = options.Value;
            _policyRegistry = policyRegistry;
            _serviceProvider = serviceProvider;
            _telemetryClient = telemetryClient;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _queueReader = new StatisticsQueueReader(
                QueueNames.Statistics,
                _options.QueueConnectionString,
                _policyRegistry,
                PolicyRegistryKey,
                _telemetryClient,
                _loggerFactory,
                _serviceProvider,
                cancellationToken);

            while (!cancellationToken.IsCancellationRequested)
            {
                await _queueReader.ProcessMessagesAsync().ConfigureAwait(false);
            }
        }
    }
}
