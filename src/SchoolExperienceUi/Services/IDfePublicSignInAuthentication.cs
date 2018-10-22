using System.Collections.Generic;
using System.Security.Claims;

namespace SchoolExperienceUi.Services
{
    public interface IDfePublicSignInAuthentication
    {
        string GenerateToken(IEnumerable<Claim> claims);
        IEnumerable<Claim> VerifyToken(string token);
    }
}
