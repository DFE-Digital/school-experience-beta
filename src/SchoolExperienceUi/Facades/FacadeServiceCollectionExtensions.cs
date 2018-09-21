using Microsoft.Extensions.DependencyInjection;
using SchoolExperienceUi.Facades;
using SchoolExperienceUi.Facades.Implementation;

namespace SchoolExperienceUi.DependencyInjection
{
    public static class FacadeServiceCollectionExtensions
    {
        public static IServiceCollection AddFacades(this IServiceCollection serviceProvider)
        {
            serviceProvider.AddScoped<ISchoolFacade, SchoolFacade>();
            serviceProvider.AddScoped<ICandidateFacade, CandidateFacade>();

            return serviceProvider;
        }
    }
}
