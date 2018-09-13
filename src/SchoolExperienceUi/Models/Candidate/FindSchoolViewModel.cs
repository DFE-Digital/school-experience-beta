using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SchoolExperienceBaseTypes;
using SchoolExperienceUi.Attributes.Validation;

namespace SchoolExperienceUi.Models.Candidate
{
    public class FindSchoolViewModel
    {
        [DisplayName("What is your post code?")]
        [Required(ErrorMessage = "A valid post code is required.")]
        [PostCode(ErrorMessage = "Please enter a full valid post code")]
        public string PostCode { get; set; }

        [DisplayName("How far do you want to search? (miles)")]
        [Range(1,99, ErrorMessage = "Please enter 1 to 99")]
        public int SearchDistanceInMiles { get; set; }

        public Distance SearchDistance { get; set; }

        public IEnumerable<School> Schools { get; set; }

        public class School
        {
            public string SchoolId { get; set; }

            [DisplayName("School")]
            public string Name { get; set; }

            [DisplayName("Contact")]
            public string ContactName { get; set; }

            [DisplayName("Address")]
            public string Address { get; set; }

            [DisplayName("Distance\n(miles)")]
            public Distance Distance { get; set; }

            [DisplayName("School type")]
            public SchoolType SchoolType { get; set; }
        }
    }
}
