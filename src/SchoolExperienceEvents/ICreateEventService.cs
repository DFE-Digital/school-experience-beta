using System;
using System.Threading.Tasks;

namespace SchoolExperienceEvents
{
    public interface ICreateEventService
    {
        Task AddBooking(string bookingId, DateTime when, string schoolId, string schoolName, string candidateId, string candidateName, string subject);
        Task ConfirmBooking(string bookingId);
        Task CancelBooking(string bookingId, string reason);

    }
}