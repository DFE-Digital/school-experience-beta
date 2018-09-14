using System;
using System.Threading.Tasks;
using SchoolExperienceApiDto.Candidate;
using SchoolExperienceBaseTypes;

namespace SchoolExperienceServices
{
    public interface ICandidateService
    {
        Task<GetDiaryEntriesResponse> GetDiaryEventsAsync(Guid userId, DateTime start, DateTime end);
        Task<CreateDiaryEntryResult> CreateDiaryEntriesAsync(Guid userId, DateTime start, DateTime end, DiaryEntryType type);
        Task<DeleteDiaryEntryResult> DeleteDiaryEntriesAsync(Guid userId, int id);
    }
}