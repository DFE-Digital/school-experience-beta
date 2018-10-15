using System.Collections.Generic;

namespace SchoolExperienceData.Entities
{
    public class Candidate
    {
        public string AccessibilityRequirements { get; set; }
        public ICollection<CandidateCalendar> Calendar { get; set; }
        public DegreeLevel Degree { get; set; }
        public string DegreeOther { get; set; }
        public Subject DegreeSubject { get; set; }
        public string ExpectationsQuestion { get; set; }
        public GitisData GitisData { get; set; }
        public string GitisReference { get; set; }
        public bool HasCriminalRecord { get; set; }
        public string HowFarQuestion { get; set; }
        public int Id { get; set; }
        public bool IsDbsChecked { get; set; }
        public Subject PreferredSubject { get; set; }
        public TeacherTrainingStatus TeacherTrainingStatus { get; set; }
        public string WhyQuestion { get; set; }
    }
}
