using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Polly;
using Polly.Caching;
using Polly.Registry;
using SchoolExperienceEvents.Implementation;

namespace SchoolExperienceNotificationProcessor
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureAppConfiguration((hostContext, configApp) =>
                {
                    configApp.SetBasePath(Directory.GetCurrentDirectory());
                    configApp.AddJsonFile("appsettings.json", optional: false);
                    configApp.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: true);
                    configApp.AddEnvironmentVariables();
                    configApp.AddCommandLine(args);
                    configApp.AddUserSecrets<NotificationServiceOptions>();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<Polly.Caching.ISyncCacheProvider<CloudQueue>, DictionaryCacheProvider<CloudQueue>>();
                    services.AddSingleton<Polly.Registry.IPolicyRegistry<string>, Polly.Registry.PolicyRegistry>((serviceProvider) =>
                    {
                        var registry = new PolicyRegistry();
                        registry.Add(NotificationService.PolicyRegistryKey, Policy.Cache<CloudQueue>(serviceProvider.GetRequiredService<ISyncCacheProvider<CloudQueue>>(), TimeSpan.FromMinutes(5)));
                        return registry;
                    });

                    Policy
                        .Handle<Exception>()
                        .WaitAndRetry(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));


                    services.AddLogging();
                    services.AddHostedService<NotificationService>();

                    services.Configure<NotificationServiceOptions>(hostContext.Configuration.GetSection(nameof(NotificationServiceOptions)));
                })
                .ConfigureLogging((hostContext, configLogging) =>
                {
                    configLogging.AddConsole();
                    configLogging.AddDebug();
                })
                //.UseConsoleLifetime()
                .Build();

            using (host)
            {
                host.Start();

                try
                {
                    host.WaitForShutdown();
                }
                catch(OperationCanceledException)
                {
                    // This is expected: CTRL+C triggers this exception.
                }
            }
        }

        private static IHostBuilder UseConfigurationSection(IHostBuilder hostBuilder, IConfiguration configuration)
        {
            foreach (var setting in configuration.AsEnumerable(true))
            {

//                hostBuilder.UseSetting(setting.Key, setting.Value);
            }

            return hostBuilder;
        }

    }
}
