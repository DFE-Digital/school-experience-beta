using System;
using SchoolExperienceBaseTypes;

namespace SchoolExperienceApiDto.Candidate
{
    public class CreateDiaryEntryRequest
    {
        public string UserId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DiaryEntryType EntryType { get; set; }
    }
}
