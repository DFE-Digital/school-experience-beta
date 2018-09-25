using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolExperienceNotificationProcessor
{
    public class NotificationServiceOptions
    {
        public string QueueConnectionString { get; set; }
        public string NotifyApiKey { get; set; }
    }
}
