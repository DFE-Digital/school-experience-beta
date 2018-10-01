using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace SchoolExperienceStatisticsData
{
    public class MigrationContextFactory
        : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var env = System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            env = "Development";
            Console.WriteLine($"Env: {env}");

            var configurationBuilder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddEnvironmentVariables();

            if (env != "Development")
            {
                configurationBuilder.AddUserSecrets("47aff185-b037-4c3f-9530-fa96ab62342e"); // <- SchoolExperienceStatisticsApi secrets id
            }
            else
            {
                configurationBuilder.AddJsonFile($"appsettings.{env}.json", optional: false);
            }

            var configuration = configurationBuilder.Build();

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            //throw new System.Exception(configuration.GetConnectionString("DefaultConnection"));

            var context = new ApplicationDbContext(builder.Options);

            return context;
        }
    }
}
