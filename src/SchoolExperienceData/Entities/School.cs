using System;
using System.Collections.Generic;

namespace SchoolExperienceData.Entities
{
    public class School
    {
        public ICollection<SchoolSchoolUserJoin> AssociatedUsers { get; set; }
        public List<SchoolCalendar> Calendar { get; set; }
        public string CandidateParkingDetails { get; set; }
        public DbsRequirement DbsRequirement { get; set; }
        public string DressCodeDetails { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int PlacementFee { get; set; }
        public TimeSpan PlacementFinishTime { get; set; }
        public TimeSpan PlacementStartTime { get; set; }
        public ICollection<Subject> PlacementSubjects { get; set; }
        public string URN { get; set; }
    }
}
