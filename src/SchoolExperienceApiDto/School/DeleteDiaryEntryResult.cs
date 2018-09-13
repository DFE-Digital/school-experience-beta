using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolExperienceApiDto.School
{
    public class DeleteDiaryEntryResult
    {
        public enum DeleteResult
        {
            None,
            Deleted,
            NotFound,
            NotAllowed,
        }

        public DeleteResult Result { get; set; }
    }
}
