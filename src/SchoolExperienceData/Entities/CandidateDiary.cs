using System;
using System.Collections.Generic;
using System.Text;
using SchoolExperienceBaseTypes;

namespace SchoolExperienceData.Entities
{
    public class CandidateDiary
    {
        public int Id { get; set; }

        public DateTime When { get; set; }

        public DiaryEntryType EntryType { get; set; }

        public School School { get; set; }

        public Candidate Candidate { get; set; }
    }
}
