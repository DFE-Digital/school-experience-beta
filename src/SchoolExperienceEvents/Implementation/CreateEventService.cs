using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SchoolExperienceEvents.AzureServices.Implementation;
using SchoolExperienceEvents.Dto;

namespace SchoolExperienceEvents.Implementation
{
    internal class CreateEventService : EventGridSenderBase, ICreateEventService
    {
        public CreateEventService(IOptions<CreateEventServiceOptions> options)
            : base(options.Value.EventHubTopicEndpoint, options.Value.EventHubTopicName)
        {
        }

        public async Task AddBooking(string bookingId, DateTime when, string schoolId, string schoolName, string candidateId, string candidateName, string subject)
        {
            var eventData = new AddBookingEvent
            {
                BookingId = bookingId,
                When = when,
                CandidateId = candidateId,
                CandidateName = candidateName,
                SchoolId = schoolId,
                SchoolName = schoolName,
                Subject = subject,
            };

            await SendAsync(EventNames.AddBooking, eventData);
        }

        public async Task ConfirmBooking(string bookingId)
        {
            var eventData = new ConfirmBookingEvent
            {
                BookingId = bookingId,
            };

            await SendAsync(EventNames.ConfirmBooking, eventData);
        }

        public async Task CancelBooking(string bookingId, string reason)
        {
            var eventData = new CancelBookingEvent
            {
                BookingId = bookingId,
                Reason = reason,
            };

            await SendAsync(EventNames.CancelBooking, eventData);
        }
    }
}
