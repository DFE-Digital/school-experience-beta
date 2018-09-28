using Microsoft.Extensions.DependencyInjection;
using SchoolExperienceServices;
using SchoolExperienceServices.Implementation;
using SchoolExperienceServices.PerformancePlatform;
using SchoolExperienceServices.PerformancePlatform.Implementation;

namespace SchoolExperience.DependencyInjection
{
    public static class ServicesServiceCollection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ISchoolService, SchoolService>();
            services.AddScoped<ICandidateService, CandidateService>();

            return services;
        }

        public static IServiceCollection AddPerformancePlatform(this IServiceCollection services)
        {
            services.AddHttpClient(PerformancePlatformService.HttpClientName);
            services.AddScoped<IPerformancePlatformService, PerformancePlatformService>();

            return services;
        }
    }
}
