using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SchoolExperienceUi.Attributes.Validation
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class PostCodeAttribute : ValidationAttribute
    {
        private readonly Regex _regex;

        // See the attribute guidelines at 
        //  http://go.microsoft.com/fwlink/?LinkId=85236
        public PostCodeAttribute()
        {
            // https://stackoverflow.com/questions/164979/uk-postcode-regex-comprehensive
            //_regex = new Regex(@"^[A-Z]{1,2}\d[A-Z\d]? ?\d[A-Z]{2}$");

            // https://assets.publishing.service.gov.uk/government/uploads/system/uploads/attachment_data/file/488478/Bulk_Data_Transfer_-_additional_validation_valid_from_12_November_2015.pdf
            _regex = new Regex(@"^([Gg][Ii][Rr] 0[Aa]{2})|((([A-Za-z][0-9]{1,2})|(([A-Za-z][A-Ha-hJ-Yj-y][0-9]{1,2})|(([AZa-z][0-9][A-Za-z])|([A-Za-z][A-Ha-hJ-Yj-y][0-9]?[A-Za-z]))))[0-9][A-Za-z]{2})$");
        }

        public override bool IsValid(object value)
        {
            var stringValue = value as string;
            if(stringValue == null || string.IsNullOrEmpty(stringValue))
            {
                return true;
            }

            stringValue = stringValue.Replace(" ", string.Empty);

            return _regex.IsMatch(stringValue);
        }
    }
}
