﻿using System;

namespace SchoolExperienceApiDto.School
{
    public class BookCandidateRequest
    {
        public string UserId { get; set; }
        public string SchoolId { get; set; }
        public string CandidateId { get; set; } 
        public DateTime Date { get; set; }
    }
}
