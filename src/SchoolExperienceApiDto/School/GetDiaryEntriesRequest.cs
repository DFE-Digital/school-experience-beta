using System;

namespace SchoolExperienceApiDto.School
{
    public class GetDiaryEntriesRequest
    {
        public DateTime End { get; set; }
        public string SchoolId { get; set; }
        public DateTime Start { get; set; }
        public string UserId { get; set; }
    }
}
