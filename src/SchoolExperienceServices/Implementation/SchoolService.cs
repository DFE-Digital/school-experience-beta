using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolExperienceApiDto.School;
using SchoolExperienceBaseTypes;

namespace SchoolExperienceServices.Implementation
{
    internal class SchoolService : ISchoolService
    {
        public async Task<FindSchoolsResult> FindSchoolsAsync(string postCode, Distance searchDistance)
        {
            var schools = new List<FindSchoolsResult.School>();

            if (searchDistance.Kilometres >= 1)
            {
                schools.Add(new FindSchoolsResult.School
                {
                    SchoolId = "1",
                    SchoolType = SchoolType.Primary,
                    Name = "St Andrew's Church of England Primary School & Nursery",
                    ContactName = "Mrs. J A Rainford",
                    Address = "Union Road, OL12 9QA",
                    Distance = new Distance { Metres = 1500 },
                });
            }

            if (searchDistance.Kilometres >= 2)
            {
                schools.Add(new FindSchoolsResult.School
                {
                    SchoolId = "2",
                    SchoolType = SchoolType.Primary,
                    Name = "Smithy Bridge Primary",
                    ContactName = "Mrs J George",
                    Address = "Bridgenorth Drive, Littleborough. OL15 0DY",
                    Distance = new Distance { Metres = 2150 },
                });
            }

            if (searchDistance.Kilometres >= 3)
            {
                schools.Add(new FindSchoolsResult.School
                {
                    SchoolId = "3",
                    SchoolType = SchoolType.Secondary,
                    Name = "Wardle Academy",
                    ContactName = "Mrs. Zoe Long",
                    Address = "Birch Rd, Rochdale OL12 9RD",
                    Distance = new Distance { Metres = 3500 },
                });
            }

            return await Task.FromResult(new FindSchoolsResult
            {
                SearchDistance = searchDistance,
                Schools = schools,
            });
        }
    }
}
