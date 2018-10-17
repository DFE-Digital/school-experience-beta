using Microsoft.Extensions.DependencyInjection;
using SchoolExperienceCandidateDataServices;
using SchoolExperienceCandidateDataServices.Implementation;

namespace SchoolExperience.DependencyInjection
{
    public static class CandidateDataServicesServiceCollectionExtensions
    {
        public static IServiceCollection AddGitisServices(this IServiceCollection services)
        {
            services.AddScoped<ICandidateDataServices, GitisServices>();

            return services;
        }
    }
}
