using SchoolExperienceBaseTypes;

namespace SchoolExperienceApiDto.School
{
    public class FindSchoolsRequest
    {
        public string PostCode { get; set; }

        public Distance Distance { get; set; }
    }
}
