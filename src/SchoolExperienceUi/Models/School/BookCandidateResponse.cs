namespace SchoolExperienceUi.Models.School
{
    public class BookCandidateResponse
    {
        public enum Status
        {
            None,
            Success,
            Conflict,
        }
        public Status Result { get; set; }
    }
}
