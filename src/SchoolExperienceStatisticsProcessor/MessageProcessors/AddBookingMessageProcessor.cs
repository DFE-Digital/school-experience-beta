using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SchoolExperienceEvents.Dto;
using SchoolExperienceStatisticsData;

namespace SchoolExperienceStatisticsProcessor.MessageProcessors
{
    internal sealed class AddBookingMessageProcessor : CounterMessageProcessorBase<AddBookingEvent>
    {
        private const string CounterName = "addbooking";

        /// <summary>
        /// Initializes a new instance of the <see cref="AddBookingMessageProcessor"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <param name="logger">The logger.</param>
        public AddBookingMessageProcessor(ApplicationDbContext dbContext, ILogger<AddBookingMessageProcessor> logger)
            : base(dbContext, logger)
        {
        }

        /// <inheritdoc />
        protected override async Task ProcessAsync(AddBookingEvent eventInfo)
        {
            await AddCounter(CounterName);
        }
    }
}
