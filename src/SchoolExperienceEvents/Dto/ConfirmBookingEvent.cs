namespace SchoolExperienceEvents.Dto
{
    public class ConfirmBookingEvent : INotificationEvent
    {
        public string BookingId { get; set; }
    }
}
