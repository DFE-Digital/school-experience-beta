using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SchoolExperienceEvents.Dto;
using SchoolExperienceProcessorShared;

namespace SchoolExperienceNotificationProcessor.MessageProcessors
{
    internal class AddBookingMessageProcessor : MessageProcessorBase<AddBookingEvent>
    {
        private readonly INotifyService _notifyService;

        public AddBookingMessageProcessor(INotifyService notifyService)
        {
            _notifyService = notifyService;
        }

        protected override async Task ProcessAsync(AddBookingEvent data)
        {
            var emailAddress = "neil.scales@transformuk.com";
            var personalisation = new Dictionary<string, object>
            {
                ["FirstName"] = data.CandidateName,
                ["SchoolName"] = data.SchoolName,
                ["BookingDate"] = data.When.ToLongDateString(),
                ["ConfirmUrl"] = "https://schoolexperiencebeta.azurewebsites.net/link/a9283b38"
            };

            await _notifyService.SendEmailAsync(emailAddress, NotifyEmailTemplates.CandidateBookingTemplate, personalisation);
        }
    }
}
