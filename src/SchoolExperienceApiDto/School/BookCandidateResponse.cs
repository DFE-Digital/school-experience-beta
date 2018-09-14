using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolExperienceApiDto.School
{
    public class BookCandidateResponse
    {
        public enum ResultStatus
        {
            None,

            Success,

            NotAvailable,

            Conflict,
        }

        public ResultStatus Status { get; set; }
    }
}
