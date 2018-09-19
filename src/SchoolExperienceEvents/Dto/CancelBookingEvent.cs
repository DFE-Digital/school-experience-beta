namespace SchoolExperienceEvents.Dto
{
    public class CancelBookingEvent : INotificationEvent
    {
        public string BookingId { get; set; }
        public string Reason { get; set; }
    }
}
