using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolExperienceNotificationProcessor
{
    public interface INotifyService
    {
        Task SendEmailAsync(string[] recipients, string templateId, Dictionary<string, object> personalisation, string groupReference);
        Task SendEmailAsync(string[] recipients, string templateId, Dictionary<string, object> personalisation);
        Task SendEmailAsync(string recipient, string templateId, Dictionary<string, object> personalisation, string groupReference);
        Task SendEmailAsync(string recipient, string templateId, Dictionary<string, object> personalisation);
    }
}
