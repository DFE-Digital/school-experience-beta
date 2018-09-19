using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.WindowsAzure.Storage.Queue;
using Polly;
using Polly.Caching;
using Polly.Registry;
using SchoolExperienceEvents;
using SchoolExperienceEvents.AzureServices.Implementation;
using SchoolExperienceEvents.Implementation;

namespace SchoolExperience.DependencyInjection
{
    public static class EventServiceServiceCollectionExtensions
    {
        public static IServiceCollection AddEventServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();

            services.AddScoped<ICreateEventService, CreateEventService>();
            services.Configure<CreateEventServiceOptions>(configuration.GetSection(nameof(CreateEventServiceOptions)));

            return services;
        }
    }
}
