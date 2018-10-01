using System;
using System.IO;
using System.Threading;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Queue;
using Polly;
using Polly.Caching;
using Polly.Registry;
using SchoolExperience.DependencyInjection;
using SchoolExperienceEvents.Implementation;

namespace SchoolExperienceStatisticsProcessor
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureAppConfiguration((hostContext, configApp) => ConfigureAppConfiguration(args, hostContext, configApp))
                .ConfigureServices(ConfigureServices)
                .ConfigureLogging(ConfigureLogging)
                .Build();

            using (host)
            {
                host.Start();
                var telemetryClient = host.Services.GetRequiredService<TelemetryClient>();
                var telemetry = new RequestTelemetry() { Name = "Processor" };
                var operation = telemetryClient.StartOperation(telemetry);

                try
                {
                    host.WaitForShutdown();
                }
                catch (OperationCanceledException)
                {
                    // This is expected: CTRL+C triggers this exception.
                }
                catch (Exception ex)
                {
                    telemetryClient.TrackException(ex);
                }
                finally
                {
                    telemetryClient.StopOperation(operation);
                    operation.Dispose();
                    Console.WriteLine("Flushing telemetry");
                    telemetryClient.Flush();    // Flush is non-blocking, so we need to wait a bit.
                    Thread.Sleep(TimeSpan.FromSeconds(5));
                    Console.WriteLine("Flushed telemetry");
                }
            }
        }

        private static void ConfigureAppConfiguration(string[] args, HostBuilderContext hostContext, IConfigurationBuilder configApp)
        {
            configApp.AddEnvironmentVariables();
            configApp.AddEnvironmentVariables(prefix: "ASPNETCORE_");
            configApp.SetBasePath(Directory.GetCurrentDirectory());
            configApp.AddJsonFile("appsettings.json", optional: false);
            //configApp.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: true);
            configApp.AddJsonFile($"appsettings.Development.json", optional: true);
            configApp.AddCommandLine(args);
            configApp.AddUserSecrets<Program>();
        }

        private static void ConfigureLogging(HostBuilderContext hostContext, ILoggingBuilder configLogging)
        {
            configLogging.AddConsole();
            configLogging.AddDebug();
        }

        private static void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services)
        {
            var configuration = hostContext.Configuration;

            services.AddSingleton<Polly.Caching.ISyncCacheProvider<CloudQueue>, DictionaryCacheProvider<CloudQueue>>();
            services.AddSingleton<Polly.Registry.IPolicyRegistry<string>, Polly.Registry.PolicyRegistry>((serviceProvider) =>
            {
                var registry = new PolicyRegistry();
                registry.Add(StatisticsService.PolicyRegistryKey, Policy.Cache<CloudQueue>(serviceProvider.GetRequiredService<ISyncCacheProvider<CloudQueue>>(), TimeSpan.FromMinutes(5)));
                return registry;
            });

            Policy
                .Handle<Exception>()
                .WaitAndRetry(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            services.AddDatabase(configuration);
            services.AddLogging();

            services.AddApplicationInsights(o =>
            {
                o.InstrumentationKey = configuration.GetValue<string>("APPINSIGHTS_INSTRUMENTATIONKEY");
                o.IncludeDiagnosticSourceActivities.Add("Microsoft.Azure.EventHubs");
                o.TelemetryInitializers.Add(new HttpDependenciesParsingTelemetryInitializer());
                o.TelemetryInitializers.Add(new OperationCorrelationTelemetryInitializer());
            });

            services.AddHostedService<StatisticsService>();

            services.Configure<StatisticsServiceOptions>(configuration.GetSection(nameof(StatisticsServiceOptions)));
        }
    }
}
