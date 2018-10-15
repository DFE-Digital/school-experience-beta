using System;

namespace SchoolExperienceData.Entities
{
    public class Booking
    {
        public System.TimeSpan? BookedDate { get; set; }
        public Candidate Candidate { get; set; }
        public string ExtraNotes { get; set; }
        public int Id { get; set; }

        public string ReportTo { get; set; }
        public System.DateTime? RequestedDate { get; set; }
        public Subject RequestedSubject { get; set; }
        public DateTime RespondBy { get; set; }
        public School School { get; set; }
        public BookingStatus Status { get; set; }
    }
}
