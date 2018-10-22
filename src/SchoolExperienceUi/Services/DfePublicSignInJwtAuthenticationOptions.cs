using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolExperienceUi.Services
{
    public class DfePublicSignInJwtAuthenticationOptions
    {
        public string ClientSecret { get; set; }
        public TimeSpan TokenExpiresAfter { get; set; }
    }
}
