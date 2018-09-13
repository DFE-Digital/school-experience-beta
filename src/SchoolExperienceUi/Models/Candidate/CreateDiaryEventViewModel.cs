using System;
using System.Collections.Generic;

namespace SchoolExperienceUi.Models.Candidate
{
    public class CreateDiaryEventViewModel
    {
        public IEnumerable<EventItem> Events { get; set; }

        public enum EventResult
        {
            None,
            Success,
            Conflict,
        }

        public class EventItem
        {
            public int Id { get; set; }
            public DateTime When { get; set; }
            public EventResult Result { get; set; }
        }
    }
}
