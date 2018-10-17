using System.Threading.Tasks;
using SchoolExperienceCandidateDataServices.Dto;

namespace SchoolExperienceCandidateDataServices
{
    public interface ICandidateDataServices
    {
        Task<SignInResult> SignIn(string credentials);
        Task<UpdateResult> UpdateName(string id, string name);
    }
}