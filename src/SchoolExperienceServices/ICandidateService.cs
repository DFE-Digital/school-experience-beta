using System;
using System.Threading.Tasks;
using SchoolExperienceApiDto.Candidate;
using SchoolExperienceBaseTypes;

namespace SchoolExperienceServices
{
    public interface ICandidateService
    {
        Task<GetDiaryEntriesResponse> GetDiaryEventsAsync(string userId, DateTime start, DateTime end);
        Task<CreateDiaryEntryResult> CreateDiaryEntriesAsync(string userId, DateTime start, DateTime end, DiaryEntryType type);
        Task<DeleteDiaryEntryResult> DeleteDiaryEntriesAsync(string userId, int id);
    }
}