using System.Threading.Tasks;
using SchoolExperienceApiDto.School;
using SchoolExperienceBaseTypes;

namespace SchoolExperienceServices
{
    public interface ISchoolService
    {
        Task<FindSchoolsResult> FindSchoolsAsync(string postCode, Distance searchDistance);
    }
}