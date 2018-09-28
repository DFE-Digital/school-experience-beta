using System;
using System.Collections.Generic;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.DependencyInjection;

namespace SchoolExperience.DependencyInjection
{
    public static class ProcessorSharedServiceCollectionExtensions
    {
        public class ApplicationInsightsConfiguration
        {
            public string InstrumentationKey { get; set; }
            public ICollection<string> ExcludeComponentCorrelationHttpHeadersOnDomains { get; } = new List<string>();
            public ICollection<string> IncludeDiagnosticSourceActivities { get; } = new List<string>();
            public ICollection<ITelemetryInitializer> TelemetryInitializers { get; } = new List<ITelemetryInitializer>();
        }

        public static IServiceCollection AddApplicationInsights(this IServiceCollection services, Action<ApplicationInsightsConfiguration> configure = null)
        {
            var config = new ApplicationInsightsConfiguration
            {
                InstrumentationKey = "<InstrumentationKey>",
            };

            configure?.Invoke(config);

            var telemetryConfig = new TelemetryConfiguration(config.InstrumentationKey);
            foreach (var initialiser in config.TelemetryInitializers)
            {
                telemetryConfig.TelemetryInitializers.Add(initialiser);
            }

            var trackingModule = new DependencyTrackingTelemetryModule();
            foreach (var domain in config.ExcludeComponentCorrelationHttpHeadersOnDomains)
            {
                trackingModule.ExcludeComponentCorrelationHttpHeadersOnDomains.Add(domain);
            }
            foreach (var activity in config.IncludeDiagnosticSourceActivities)
            {
                trackingModule.IncludeDiagnosticSourceActivities.Add(activity);
            }

            trackingModule.Initialize(telemetryConfig);

            services.AddSingleton<ITelemetryModule>(trackingModule);
            services.AddSingleton(new TelemetryClient(telemetryConfig));

            return services;
        }
    }
}
