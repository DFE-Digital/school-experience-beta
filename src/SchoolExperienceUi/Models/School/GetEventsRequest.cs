using System;

namespace SchoolExperienceUi.Models.School
{
    public class GetEventsRequest
    {
        public string SchoolId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
