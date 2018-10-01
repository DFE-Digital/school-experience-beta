using System;
using System.Threading.Tasks;
using SchoolExperienceEvents.Dto;

namespace SchoolExperienceProcessorShared
{
    public interface IMessageProcessor : IDisposable
    {
        Task ProcessAsync(INotificationEvent message);
    }
}