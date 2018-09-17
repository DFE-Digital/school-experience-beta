﻿namespace SchoolExperienceUi.Models.School
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
        public string Text { get; set; }
        public int Id { get; set; }
    }
}
