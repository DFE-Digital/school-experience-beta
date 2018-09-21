using System.Collections.Generic;

namespace SchoolExperienceApiDto.School
{
    public class GetPlacementTotalsResponse
    {
        public IEnumerable<SchoolPlacement> SchoolPlacements { get; set; }

        public IEnumerable<SubjectPlacement> SubjectPlacements { get; set; }

        public int TotalAdvertised { get; set; }

        public int TotalAvailable { get; set; }

        public int TotalBooked { get; set; }

        public class SchoolPlacement
        {
            public int Count { get; set; }
            public string Name { get; set; }
        }

        public class SubjectPlacement
        {
            public int Count { get; set; }
            public string Name { get; set; }
        }
    }
}
