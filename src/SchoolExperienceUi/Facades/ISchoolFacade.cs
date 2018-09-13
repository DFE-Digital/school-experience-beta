using System.Threading.Tasks;
using SchoolExperienceApiDto.School;
using SchoolExperienceBaseTypes;

namespace SchoolExperienceUi.Facades
{
    public interface ISchoolFacade
    {
        Task<FindSchoolsResult> FindSchoolsAsync(string postCode, Distance searchDistance);
    }
}
