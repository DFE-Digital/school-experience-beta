using System;

namespace SchoolExperienceUi.Models.School
{
    public class BookCandidateRequest
    {
        public string SchoolId { get; set; }
        public string CandidateId { get; set; }
        public DateTime When { get; set; }
    }
}
