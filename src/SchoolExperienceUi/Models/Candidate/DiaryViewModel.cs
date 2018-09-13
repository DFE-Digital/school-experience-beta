using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolExperienceUi.Models.Candidate
{
    public class DiaryViewModel
    {
        public IEnumerable<DiaryViewModelEvent> Events { get; set; }
    }

    public class DiaryViewModelEvent
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool AllDay { get; set; }
        public bool IsFree { get; set; }
        public bool IsBusy { get; set; }
    }
}
