using System;

namespace SchoolExperienceData.Entities
{
    public class Notification
    {
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets when the message was sent to Notify (might not be when it was actually sent to the user).
        /// </summary>
        public DateTime Sent { get; set; }

        /// <summary>
        /// Gets or sets the notify template identifier.
        /// </summary>
        public string TemplateId { get; set; }

        /// <summary>
        /// Gets or sets the group reference for one email sent to different addresses.
        /// </summary>
        public string SendGroupReference { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the message sent by Notify.
        /// </summary>
        public string NotificationId { get; set; }
    }
}
