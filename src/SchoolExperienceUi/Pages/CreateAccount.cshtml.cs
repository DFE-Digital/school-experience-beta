using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using SchoolExperienceUi.Services;

namespace SchoolExperienceUi.Pages
{
    public class CreateAccountModel : PageModel
    {
        private readonly IDfePublicSignOn _signOn;
        private readonly IDfePublicSignInAuthentication _authentication;

        private readonly CreateAccountModelOptions _options;

        public CreateAccountModel(IDfePublicSignOn signOn, IDfePublicSignInAuthentication authentication, IOptions<CreateAccountModelOptions> options)
        {
            _signOn = signOn;
            _authentication = authentication;
            _options = options.Value;
        }
            
        public async Task OnGetAsync()
        {
            var emailAddress = "email@example.com";
            var givenName = "first";
            var familyName = "last";

            var claims = new List<Claim>();
            claims.Add(new Claim("email", emailAddress));

            var token = _authentication.GenerateToken(claims);
            var result = await _signOn.CreateAccount(token, _options.ClientId, emailAddress, givenName, familyName);

            var resultClaims = _authentication.VerifyToken(result.JwtToken);

        }
    }
}