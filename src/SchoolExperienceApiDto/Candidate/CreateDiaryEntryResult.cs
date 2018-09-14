using System;
using System.Collections.Generic;
using SchoolExperienceBaseTypes;

namespace SchoolExperienceApiDto.Candidate
{
    public class CreateDiaryEntryResult
    {
        public IEnumerable<EventItem> Events { get; set; }

        public class EventItem
        {
            public int Id { get; set; }
            public CreateEventResult Result { get; set; }
            public DateTime When { get; set; }
        }
    }
}
