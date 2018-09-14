using System;
using System.Threading.Tasks;
using SchoolExperienceApiDto.School;
using SchoolExperienceBaseTypes;

namespace SchoolExperienceServices
{
    public interface ISchoolService
    {
        Task<FindSchoolsResponse> FindSchoolsAsync(string postCode, Distance searchDistance);
        Task<BookCandidateResponse> CreateBooking(string userId, string schoolId, string candidateId, DateTime when);
        Task<GetDiaryEntriesResponse> GetDiaryEventsAsync(string userId, string schoolId, DateTime start, DateTime end);
    }
}