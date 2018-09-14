using System;
using System.Collections.Generic;
using SchoolExperienceBaseTypes;

namespace SchoolExperienceApiDto.School
{
    public class GetDiaryEntriesResponse
    {
        public IEnumerable<DiaryEvent> Events { get; set; }

        public class DiaryEvent
        {
            public int Id { get; set; }
            public string CandidateName { get; set; } 
            public string CandidateSubject { get; set; }
            public DiaryEntryType EntryType { get; set; }
            public DateTime When { get; set; }
        }
    }
}
