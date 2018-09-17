using System;

namespace SchoolExperienceEvents.Dto
{
    public class AddBookingEvent
    {
        public DateTime When { get; set; }
        public string SchoolId { get; set; }
        public string SchoolName { get; set; }
        public string CandidateId { get; set; }
        public string CandidateName { get; set; }
        public string Subject { get; set; }
    }
}