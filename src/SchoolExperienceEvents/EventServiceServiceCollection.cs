using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage.Queue;
using Polly;
using Polly.Caching;
using Polly.Registry;
using SchoolExperienceEvents;
using SchoolExperienceEvents.Implementation;

namespace SchoolExperience.DependencyInjection
{
    public static class EventServiceServiceCollection
    {
        public static IServiceCollection AddEventServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();

            services.AddSingleton<Polly.Caching.ISyncCacheProvider<CloudQueue>, DictionaryCacheProvider<CloudQueue>>();
            services.AddSingleton<Polly.Registry.IPolicyRegistry<string>, Polly.Registry.PolicyRegistry>((serviceProvider) =>
            {
                var registry = new PolicyRegistry();
                registry.Add(EventServiceBase.PolicyRegistryKey, Policy.Cache<CloudQueue>(serviceProvider.GetRequiredService<ISyncCacheProvider<CloudQueue>>(), TimeSpan.FromMinutes(5)));
                return registry;
            });

            Policy
                .Handle<Exception>()
                .WaitAndRetry(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            services.AddScoped<ICreateEventService, CreateEventService>();
            services.Configure<CreateEventServiceOptions>(configuration.GetSection(nameof(CreateEventServiceOptions)));

            return services;
        }
    }
}
