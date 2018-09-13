using System;
using System.Collections.Generic;

namespace SchoolExperienceUi.Models.Candidate
{
    public class IndexViewModel
    {
        public IEnumerable<IndexBookingItem> Bookings { get; set; }
    }

    public class IndexBookingItem
    {
        public DateTime When { get; set; }
        public string SchoolName { get; set; }
    }
}
