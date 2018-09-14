namespace SchoolExperienceApiDto.Candidate
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
