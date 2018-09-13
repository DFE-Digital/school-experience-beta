using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace SchoolExperienceData.Seed
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider services)
        {
            try
            {
                var dbContext = services.GetService<ApplicationDbContext>();

                if (!dbContext.Candidates.Any())
                {
                    Console.WriteLine("Seed candidates");
                    dbContext.Candidates.Add(new Entities.Candidate
                    {
                        Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                        Name = "Test User",
                    });

                    dbContext.SaveChanges();
                }

                if (!dbContext.Schools.Any())
                {
                    Console.WriteLine("Seed schools");
                    dbContext.Schools.Add(new Entities.School
                    {
                        Id = "22222222-2222-2222-2222-222222222222",
                        Name = "School #1",
                    });

                    dbContext.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Seed: {ex}");
                throw;
            }
        }
    }
}
