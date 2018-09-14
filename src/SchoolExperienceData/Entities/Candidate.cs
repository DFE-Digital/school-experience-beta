using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolExperienceData.Entities
{
    public class Candidate
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Subject { get; set; }

        public ICollection<CandidateDiary> Events { get; set; }
    }
}
