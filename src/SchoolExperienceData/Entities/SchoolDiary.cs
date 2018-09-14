using System;

namespace SchoolExperienceData.Entities
{
    public class SchoolDiary
    {
        public CandidateDiary CandidateDiary { get; set; }

        public int Id { get; set; }

        public School School { get; set; }

        public DateTime When { get; set; }
    }
}
