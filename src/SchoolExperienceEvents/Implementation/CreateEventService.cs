using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Polly.Registry;
using SchoolExperienceEvents.Dto;

namespace SchoolExperienceEvents.Implementation
{
    internal class CreateEventService : EventServiceBase, ICreateEventService
    {
        private const string AddBookingQueueName = "addbooking";

        public CreateEventService(IOptions<CreateEventServiceOptions> options, IPolicyRegistry<string> policyRegistry)
            : base(options, policyRegistry)
        {
        }

        public async Task AddBooking(DateTime when, string schoolId, string schoolName, string candidateId, string candidateName, string subject)
        {
            var eventData = new AddBookingEvent
            {
                When = when,
                CandidateId = candidateId,
                CandidateName = candidateName,
                SchoolId = schoolId,
                SchoolName = schoolName,
                Subject = subject,
            };

            await AddMessageAsync(AddBookingQueueName, eventData);
        }
    }
}
