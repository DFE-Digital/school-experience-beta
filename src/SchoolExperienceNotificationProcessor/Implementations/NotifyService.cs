using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Notify.Client;
using SchoolExperienceData;
using SchoolExperienceHelpers;

namespace SchoolExperienceNotificationProcessor.Implementations
{
    internal class NotifyService : INotifyService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly NotificationClient _notifyService;
        private readonly ILogger<NotifyService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotifyService"/> class.
        /// </summary>
        /// <param name="notifyService">The notify service.</param>
        /// <param name="dbContext">The database context.</param>
        /// <param name="logger">The logger.</param>
        public NotifyService(NotificationClient notifyService, ApplicationDbContext dbContext, ILogger<NotifyService> logger)
        {
            _notifyService = notifyService;
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task SendEmailAsync(string[] recipients, string templateId, Dictionary<string, object> personalisation, string groupReference)
        {
            if (groupReference == null)
            {
                groupReference = Guid.NewGuid().ToString();
            }

            var now = DateTime.UtcNow;

            foreach (var recipient in recipients)
            {
                _logger.LogTrace($"Email: E:{EmailHelpers.AnonymiseEmailAddress(recipient)}, T:{templateId}, G:{groupReference}");

                var response = _notifyService.SendEmail(recipient, templateId, personalisation, groupReference);

                _logger.LogInformation($"Email response id: {response.id}");
                var notification = new SchoolExperienceData.Entities.Notification
                {
                    EmailAddress = recipient,
                    NotificationId = response.id,
                    SendGroupReference = groupReference,
                    Sent = now,
                    TemplateId = templateId,
                };

                _dbContext.Notifications.Add(notification);
            }

            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task SendEmailAsync(string[] recipients, string templateId, Dictionary<string, object> personalisation)
        {
            await SendEmailAsync(recipients, templateId, personalisation, null).ConfigureAwait(false);
        }

        public async Task SendEmailAsync(string recipient, string templateId, Dictionary<string, object> personalisation, string groupReference)
        {
            await SendEmailAsync(new[] { recipient }, templateId, personalisation, groupReference).ConfigureAwait(false);
        }

        public async Task SendEmailAsync(string recipient, string templateId, Dictionary<string, object> personalisation)
        {
            await SendEmailAsync(new[] { recipient }, templateId, personalisation, null).ConfigureAwait(false);
        }
    }
}
