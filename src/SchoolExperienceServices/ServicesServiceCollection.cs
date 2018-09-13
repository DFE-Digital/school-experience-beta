using Microsoft.Extensions.DependencyInjection;
using SchoolExperienceServices;
using SchoolExperienceServices.Implementation;

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
    }
}
