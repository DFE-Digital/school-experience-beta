using System;
using System.Threading.Tasks;

namespace SchoolExperienceEvents
{
    public interface ICreateEventService
    {
        Task AddBooking(DateTime when, string schoolId, string schoolName, string candidateId, string candidateName, string subject);
    }
}