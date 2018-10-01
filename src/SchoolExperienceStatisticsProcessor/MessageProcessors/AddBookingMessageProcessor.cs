using System;
using System.Threading.Tasks;
using SchoolExperienceEvents.Dto;
using SchoolExperienceProcessorShared;

namespace SchoolExperienceStatisticsProcessor.MessageProcessors
{
    internal sealed class AddBookingMessageProcessor : MessageProcessorBase<AddBookingEvent>
    {
        private static readonly Random Random = new Random();

        protected override Task ProcessAsync(AddBookingEvent eventInfo)
        {
            if (Random.Next(4) == 0)
            {
                throw new ArgumentException("Random error");
            }

            return Task.CompletedTask;
        }
    }
}
