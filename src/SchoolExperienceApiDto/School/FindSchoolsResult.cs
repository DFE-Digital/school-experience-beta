using System.Collections.Generic;
using SchoolExperienceBaseTypes;

namespace SchoolExperienceApiDto.School
{
    public class FindSchoolsResult
    {
        public Distance SearchDistance { get; set; }
        public IEnumerable<School> Schools { get; set; }

        public class School
        {
            public string SchoolId { get; set; }
            public string Name { get; set; }
            public string ContactName { get; set; }
            public string Address { get; set; }
            public Distance Distance { get; set; }
            public SchoolType SchoolType { get; set; }
        }
    }
}
