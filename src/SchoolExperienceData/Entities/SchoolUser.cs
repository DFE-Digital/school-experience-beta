using System.Collections.Generic;

namespace SchoolExperienceData.Entities
{
    public class SchoolUser
    {
        public ICollection<SchoolSchoolUserJoin> Associations { get; set; }
        public string DfeReference { get; set; }
        public int Id { get; set; }
    }
}
