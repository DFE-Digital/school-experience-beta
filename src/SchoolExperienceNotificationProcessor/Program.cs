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
using Notify.Client;
using Polly;
using Polly.Caching;
using Polly.Registry;
using SchoolExperience.DependencyInjection;
using SchoolExperienceEvents.Implementation;
using SchoolExperienceNotificationProcessor.Implementations;

namespace SchoolExperienceNotificationProcessor
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureAppConfiguration((hostContext, configApp) =>
                {
                    configApp.AddEnvironmentVariables();
                    configApp.AddEnvironmentVariables(prefix: "ASPNETCORE_");
                    configApp.SetBasePath(Directory.GetCurrentDirectory());
                    configApp.AddJsonFile("appsettings.json", optional: false);
                    //configApp.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: true);
                    configApp.AddJsonFile($"appsettings.Development.json", optional: true);
                    configApp.AddCommandLine(args);
                    configApp.AddUserSecrets<NotificationServiceOptions>();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    var configuration = hostContext.Configuration;

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

                    services.AddDatabase(configuration);

                    var key = configuration.GetValue<string>("NotifyApiKey");

                    services.AddScoped<NotificationClient>(sp => new NotificationClient(configuration.GetValue<string>("NotifyApiKey")));
                    services.AddScoped<INotifyService, NotifyService>();

                    services.AddLogging();
                    services.AddHostedService<NotificationService>();

                    services.Configure<NotificationServiceOptions>(configuration.GetSection(nameof(NotificationServiceOptions)));
                })
                .ConfigureLogging((hostContext, configLogging) =>
                {
                    configLogging.AddConsole();
                    configLogging.AddDebug();
                })
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
