using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolExperienceStatisticsData.Entities
{
    public class Counter
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [ConcurrencyCheck]
        public int Count { get; set; }

        [ConcurrencyCheck]
        public DateTime LastUpdated { get; set; }
    }
}
