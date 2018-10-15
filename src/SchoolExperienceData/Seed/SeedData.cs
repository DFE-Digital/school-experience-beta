using System;
using System.Collections.Generic;
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
                    dbContext.GitisUsers.Add(new Entities.GitisData
                    {
                        Id = "11111111-1111-1111-1111-111111111111",
                        Name = "Test User",
                        EmailAddress = "user1@example.com",
                        Address = "1 High Street, Lowtown",
                    });
                    dbContext.Candidates.Add(new Entities.Candidate
                    {
                        GitisReference = "11111111-1111-1111-1111-111111111111",
                    });

                    dbContext.SaveChanges();
                }

                if (!dbContext.Schools.Any())
                {
                    Console.WriteLine("Seed schools");
                    dbContext.Schools.Add(new Entities.School
                    {
                        Name = "School #1",
                        URN = "12345678",
                    });

                    dbContext.SaveChanges();
                }

                if(!dbContext.SchoolUsers.Any())
                {
                    Console.WriteLine("Seed school users");
                    var schoolUser1 = new Entities.SchoolUser
                    {
                        DfeReference = "neil.scales@education.gov.uk",
                        Associations = new List<Entities.SchoolSchoolUserJoin>()
                    };

                    dbContext.SchoolUsers.Add(schoolUser1);

                    schoolUser1.Associations.Add(
                        new Entities.SchoolSchoolUserJoin
                        {
                            School = dbContext.Schools.First(x => x.URN == "12345678"),
                            SchoolUser = schoolUser1,
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
