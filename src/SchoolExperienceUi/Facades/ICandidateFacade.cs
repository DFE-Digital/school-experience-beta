using System;
using System.Threading.Tasks;
using SchoolExperienceApiDto.Candidate;
using SchoolExperienceBaseTypes;

namespace SchoolExperienceUi.Facades
{
    public interface ICandidateFacade : IFacade
    {
        Task<GetDiaryEntriesResponse> GetDiaryEntriesAsync(string userId, DateTime start, DateTime end);
        Task<CreateDiaryEntryResult> CreateDiaryEntryAsync(string userId, DateTime start, DateTime end, DiaryEntryType free);
        Task<DeleteDiaryEntryResult> DeleteDiaryEntryAsync(string userId, int id);
    }
}
