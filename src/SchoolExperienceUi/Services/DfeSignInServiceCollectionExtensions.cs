using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchoolExperienceUi.Services;

namespace SchoolExperienceUi.DependencyInjection
{
    public static class DfeSignInServiceCollectionExtensions
    {
        public static IServiceCollection AddDfeSignIn(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDfePublicSignInAuthentication, DfePublicSignInJwtAuthentication>();
            services.AddScoped<IDfePublicSignOn, DfePublicSignOn>();
            services.Configure<DfePublicSignInJwtAuthenticationOptions>(configuration.GetSection(nameof(DfePublicSignInJwtAuthenticationOptions)));
            services.Configure<DfePublicSignOnOptions>(configuration.GetSection(nameof(DfePublicSignOnOptions)));

            return services;
        }
    }
}
