namespace SchoolExperienceApiDto.School
{
    public class CancelBookingResponse
    {
        public enum Status
        {
            None,
            Success,
            UnknownEvent,
        }

        public Status Result { get; set; }
    }
}
