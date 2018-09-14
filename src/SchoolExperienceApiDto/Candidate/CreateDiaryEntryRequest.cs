using System;
using System.Collections.Generic;
using System.Text;
using SchoolExperienceBaseTypes;

namespace SchoolExperienceApiDto.Candidate
{
    public class CreateDiaryEntryRequest
    {
        public Guid UserId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DiaryEntryType EntryType { get; set; }
    }
}
