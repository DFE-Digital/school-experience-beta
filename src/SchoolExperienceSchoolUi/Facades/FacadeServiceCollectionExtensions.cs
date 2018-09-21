using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SchoolExperienceSchoolUi.Facades;
using SchoolExperienceSchoolUi.Facades.Implementation;

namespace SchoolExperienceSchoolUi.DependencyInjection
{
    public static class FacadeServiceCollectionExtensions
    {
        public static IServiceCollection AddFacades(this IServiceCollection serviceProvider)
        {
            serviceProvider.AddScoped<ISchoolFacade, SchoolFacade>();

            return serviceProvider;
        }
    }
}
