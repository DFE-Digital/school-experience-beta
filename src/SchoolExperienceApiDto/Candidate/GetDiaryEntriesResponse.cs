using System;
using System.Collections.Generic;
using System.Text;
using SchoolExperienceBaseTypes;

namespace SchoolExperienceApiDto.Candidate
{
    public class GetDiaryEntriesResponse
    {
        public IEnumerable<DiaryEvent> Events { get; set; }

        public class DiaryEvent
        {
            public int Id { get; set; }
            public string Title { get; set; } 
            public DiaryEntryType EntryType { get; set; }
            public DateTime When { get; set; }
        }
    }
}
