using System;
using System.Threading.Tasks;
using SchoolExperienceApiDto.School;
using SchoolExperienceBaseTypes;

namespace SchoolExperienceServices
{
    public interface ICandidateService
    {
        Task<GetDiaryEntriesResult> GetDiaryEventsAsync(Guid userId, DateTime start, DateTime end);
        Task<CreateDiaryEntryResult> CreateDiaryEntriesAsync(Guid userId, DateTime start, DateTime end, DiaryEntryType type);
        Task<DeleteDiaryEntryResult> DeleteDiaryEntriesAsync(Guid userId, int id);
    }
}