using System.Threading.Tasks;

namespace SchoolExperienceUi.Services
{
    public interface IDfePublicSignOn
    {
        Task<CreateAccountResult> CreateAccount(string jwtToken, string clientId, string emailAddress, string givenName, string familyName);
    }
}