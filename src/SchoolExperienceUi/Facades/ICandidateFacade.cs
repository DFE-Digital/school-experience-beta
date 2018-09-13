using System;
using System.Threading.Tasks;
using SchoolExperienceApiDto.School;
using SchoolExperienceBaseTypes;

namespace SchoolExperienceUi.Facades
{
    public interface ICandidateFacade : IFacade
    {
        Task<GetDiaryEntriesResult> GetDiaryEntriesAsync(Guid userId, DateTime start, DateTime end);
        Task<CreateDiaryEntryResult> CreateDiaryEntryAsync(Guid userId, DateTime start, DateTime end, DiaryEntryType free);
        Task<DeleteDiaryEntryResult> DeleteDiaryEntryAsync(Guid userId, int id);
    }
}
