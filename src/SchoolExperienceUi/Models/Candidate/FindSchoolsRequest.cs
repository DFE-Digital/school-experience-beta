using SchoolExperienceBaseTypes;

namespace SchoolExperienceUi.Models.Candidate
{
    public class FindSchoolsRequest
    {
        public string PostCode { get; set; }

        public Distance Distance { get; set; }
    }
}
