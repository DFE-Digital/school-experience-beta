using System;
using System.Threading.Tasks;
using SchoolExperienceApiDto.School;
using SchoolExperienceBaseTypes;

namespace SchoolExperienceUi.Facades
{
    public interface ISchoolFacade
    {
        Task<FindSchoolsResponse> FindSchoolsAsync(string postCode, Distance searchDistance);
        Task<GetDiaryEntriesResponse> GetDiaryEntriesAsync(string userId, string schoolId, DateTime start, DateTime end);
        Task<BookCandidateResponse> BookCandidate(string userId, string schoolId, string candidateId, DateTime when);
        Task<CancelBookingResponse> CancelBooking(string userId, int id);
    }
}
